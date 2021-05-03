<template>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark sticky-top">
        <div class="container-lg">
            <a class="navbar-brand" :href="baseUrl">
                {{appName}}                
                <div class="small">{{branchStoreName}}</div>
            </a>
            <!--<b-navbar-toggle target="navDrawer"></b-navbar-toggle>-->

            <div class="collapse navbar-collapse" id="navbarColor01">
                <b-navbar-nav>
                    <template v-for="menu in menus">
                        <b-nav-item v-if="!menu.subMenus" :to="menu.to">
                            <span v-bind:class="menu.icon"></span>&nbsp;{{menu.label}}
                        </b-nav-item>
                        <b-nav-item-dropdown v-else>
                            <template v-slot:button-content>
                                <span v-bind:class="menu.icon"></span>&nbsp;{{menu.label}}
                            </template>
                            <b-dropdown-item v-for="mnu in menu.subMenus" :to="mnu.to">
                                <span class="mr-1" v-bind:class="mnu.icon"></span>{{mnu.label}}
                            </b-dropdown-item>

                        </b-nav-item-dropdown>
                    </template>
                </b-navbar-nav>
            </div>

            <ul class="navbar-nav ml-auto flex-row">
                <li v-show="messages.length>0" class="nav-item">
                    <a v-b-toggle.messagesDrawer class="nav-link" @click.prevent href="#" role="button">
                        <i class="fas fa-envelope fa-fw"></i>
                        <span class="badge badge-danger">
                            <span>{{messages.length}}</span>
                        </span>
                    </a>
                </li>
                <li v-show="notifications.length>0" class="nav-item px-2 px-sm-0">
                    <a v-b-toggle.notificationsDrawer class="nav-link" @click.prevent href="#">
                        <i class="fas fa-fw fa-bell"></i>
                        <span class="badge badge-danger">
                            <span>{{notifications.length}}</span>
                        </span>
                    </a>
                </li>

                <!-- Nav Item - User Information -->
                <li class="nav-item px-2 px-sm-0 dropdown">
                    <a class="nav-link  dropdown-toggle" href="#" id="userDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                        <b-avatar variant="info" size="sm" :src="urlProfilePicture"></b-avatar>
                    </a>
                    <!-- Dropdown - User Information -->
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="userDropdown" style="position:absolute">
                        <router-link to="/accounts" class="dropdown-item">
                            <i class="fas fa-user fa-sm fa-fw mr-2"></i>
                            Account
                        </router-link>

                        <!--<div class="dropdown-divider"></div>

            <router-link to="/pharmacy" class="dropdown-item">
                <i class="fas fa-home fa-sm fa-fw mr-2"></i>
                Pharmacy Info
            </router-link>-->

                        <div class="dropdown-divider"></div>

                        <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">
                            <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 "></i>
                            Logout
                        </a>
                    </div>
                </li>
            </ul>
        </div>
    </nav>
</template>
<script>
    import navbarMixin from '../../../_Core/Mixins/navbarMixin';

    export default {
        mixins: [
            navbarMixin
        ],
        props: {
            appName: {
                type: String,
                default: 'WaterPro [Owner]'
            },
            urlProfilePicture: String,
            menus: Array,
            baseUrl: String,
            email: String,
            uid: String,
            branchStoreId: { type: String, required: true },
            branchStoreName: { type: String, required: true },
        },
        data() {
            return {
                membership: {
                    roles: []
                },
                notifications: {
                    items: [],
                    totalCount: 0
                },
                //branchStore: {}
            };
        },
        computed: {
            returnUrl() {
                let path = location.pathname

                if (path.includes('/account/logout'))
                    path = '/';

                return path;
            },            
        },

        async mounted() {
            const vm = this;



            //vm.$bus.$on('event:membership', (membership) => {
            //    vm.membership = membership;
            //});

            vm.$bus.$on('request:get-notifications', async (organizationId, uid) => {
                const vm = this;

                //vm.membership = await vm.getMembershipInfo(organizationId);

                await vm.getNotifications(organizationId, uid);


            });

            //if (vm.authenticated) {
            //    await vm.getNotifications();
            //}


        },
        methods: {
            //getCookie(cname) {
            //    var name = cname + "=";
            //    var decodedCookie = decodeURIComponent(document.cookie);
            //    var ca = decodedCookie.split(';');
            //    for (var i = 0; i < ca.length; i++) {
            //        var c = ca[i];
            //        while (c.charAt(0) == ' ') {
            //            c = c.substring(1);
            //        }
            //        if (c.indexOf(name) == 0) {
            //            return c.substring(name.length, c.length);
            //        }
            //    }
            //    return "";
            //},
            async getNotifications(organizationId, uid) {
                const vm = this;
                const filter = {
                    query: {
                        pageIndex: 1,
                        pageSize: 10,
                        sortField: '',
                        sortOrder: -1
                    }
                };

                if (vm.busy)
                    return;

                const query = [
                    '?c=', //encodeURIComponent(filter.query.criteria),
                    '&p=', filter.query.pageIndex,
                    '&s=', filter.query.pageSize,
                    '&sf=', filter.query.sortField,
                    '&so=', filter.query.sortOrder,
                    '&organizationId=', organizationId,
                    '&receiverId=', uid,
                    '&unreadOnly=true'
                ].join('');

                vm.busy = true;

                try {
                    await vm.$util.axios.get(`/api/notifications/current/${query}`)
                        .then(resp => {
                            vm.notifications = resp.data;
                        });

                } catch (e) {
                    //vm.$util.handleError(e);
                }

            },

            async getMembershipInfo(organizationId) {
                const vm = this;

                try {
                    const uid = vm.authenticated ? vm.uid : '!';

                    return await vm.$util.axios.get(`api/organizations/${organizationId}/membership-info/${uid}`).
                        then(resp => {
                            const data = resp.data;
                            //vm.membership = data;

                            //if (data.status === 2) {
                            var isAdmin = data.roles.find(e => e.roleId === 'organizationadministrator') !== undefined;

                            data.isAdmin = data.status === 2 && isAdmin;
                            data.isMember = data.status === 2 && !isAdmin;
                            data.isAdminOrMember = data.status === 2 && (data.isAdmin || data.isMember);
                            //}

                            return data;
                            //vm.$bus.$emit('event:membership', data);
                        });
                } catch (e) {
                    //vm.$util.handleError(e);
                }

            },
        }
    };

</script>