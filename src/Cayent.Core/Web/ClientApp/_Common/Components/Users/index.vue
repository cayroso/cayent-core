<style></style>
<template>
    <div v-cloak>
        <div class="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center">
            <div>
                <h1 class="h3 mb-sm-0">
                    <span class="fas fa-fw fa-users-cog mr-1"></span>Users
                </h1>
            </div>
            <div class="d-flex flex-row justify-content-end">

                <div>
                    <button @click="add" class="btn btn-primary ml-1">
                        <span class="fas fa-fw fa-plus"></span>
                    </button>
                </div>
                <div class="ml-1">
                    <div class="input-group">
                        <input v-model="filter.criteria" @keyup.enter="search(1)" type="text" class="form-control">
                        <div class="input-group-append">
                            <button @click="search(1)" class="btn btn-secondary" type="button" id="button-addon2">
                                <span class="fa fas fa-fw fa-search"></span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <b-overlay :show="busy">
            <div class="mt-2 ">
                <div class="table-responsive">
                    <table class="table table-bordered">
                        <thead>
                            <tr class="bg-success">
                                <td>#</td>
                                <td>First Name</td>
                                <td>Last Name</td>
                                <td>Email</td>
                                <td>Phone</td>
                                <td>Role(s)</td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item,i) in filter.items">
                                <td>{{getRowNumber(i)}}</td>
                                <td>{{item.firstName}}</td>
                                <td>{{item.lastName}}</td>
                                <td>{{item.email}}</td>
                                <td>{{item.phoneNumber}}</td>
                                <td>{{item.roles.join(', ')}}</td>
                                <td class="text-center aling-middle">
                                    <button @click="view(item.userId)" class="btn btn-sm btn-outline-primary">
                                        <span class="fas fa-fw fa-eye"></span>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </b-overlay>

        <div class="mt-2">
            <m-pagination :filter="filter" :search="search"></m-pagination>
        </div>


    </div>
</template>
<script>
    import pageMixin from '../../Mixins/pageMixin';
    import paginatedMixin from '../../Mixins/paginatedMixin';
    export default {
        mixins: [pageMixin, paginatedMixin],
        props: {
            uid: String,
            durl: String,
            rurl: String,
            urlAdd: String,
            urlView: String
        },
        data() {
            return {
                filterCacheKey: `filter-${this.uid}/users/index`,
            };
        },
        async created() {
            const vm = this;
            const filter = vm.filter;

            const cache = JSON.parse(localStorage.getItem(vm.filterCacheKey)) || {};
            const urlParams = new URLSearchParams(window.location.search);

            vm.initializeFilter(cache);

            await vm.search(0);

        },
        methods: {
            async _search() {
                const vm = this;
                const filter = vm.filter;

                if (vm.busy)
                    return;

                const query = [
                    '?c=', encodeURIComponent(filter.criteria),
                    '&p=', filter.pageIndex,
                    '&s=', filter.pageSize,
                    '&sf=', filter.sortField,
                    '&so=', filter.sortOrder
                ].join('');

                vm.busy = true;
                await vm.search_internal(`api/manager/users`, query, 'Users',
                    () => {
                        localStorage.setItem(vm.filterCacheKey, JSON.stringify({
                            pageSize: filter.pageSize,
                            sortField: filter.sortField,
                            sortOrder: filter.sortOrder,
                            visible: filter.visible
                        }));

                        filter.items.forEach(item => {
                            vm.$set(item, 'expand', false);
                        });

                    }, () => {
                        localStorage.removeItem(vm.filterCacheKey);
                    }, () => {
                        vm.busy = false;
                    });
            },
            add() {
                const vm = this;
                const rUrl = btoa(window.location.href);
                const url = `${vm.urlAdd}?r=${rUrl}`;

                vm.$util.href(url);
            },

            view(userId) {
                const vm = this;
                const rUrl = btoa(window.location.href);
                const url = `${vm.urlView}${userId}?r=${rUrl}`;

                vm.$util.href(url);
            },
        }
    }
</script>