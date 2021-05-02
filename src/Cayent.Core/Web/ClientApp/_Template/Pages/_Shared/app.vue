<template>
    <div v-cloak>
        <app-bar :uid="uid" :appName="appName" :urlProfilePicture="urlProfilePicture" :menus="menus"></app-bar>
        <main class="container-lg main" v-bind:style="bottomNavHeightStyle">
            <router-view :uid="uid"></router-view>
        </main>

        <bottom-nav ref="bottomNav" :menus="menus"></bottom-nav>
        <!--<modal-view-chat ref="modalViewChat" :uid="uid"></modal-view-chat>-->
    </div>
</template>
<script>
    'use strict';
    import appMixin from '../../../_Core/Mixins/appMixin';
    //import SystemBar from './system-bar.vue';
    import AppBar from './app-bar.vue';
    import NavDrawer from './nav-drawer.vue';
    //import AppFooter from './footer.vue';
    import BottomNav from './bottom-nav.vue';
    //import ReturnBottle from '../../../_Core/Modals/Bottles/return-bottle.vue';

    //import CustomerOrdersAdd from '../CustomerOrders/Add/index.vue';
    //import CustomerOrdersView from '../CustomerOrders/View/index.vue';
    //import CustomerOrdersIndex from '../CustomerOrders/index.vue';


    //import CustomersAdd from '../Customers/add.vue';
    //import CustomersIndex from '../Customers/index.vue';
    //import CustomersView from '../Customers/view.vue';

    //import OwnerIndex from '../Index/index.vue';


    export default {
        mixins: [appMixin],
        components: {
            //SystemBar,
            AppBar,
            NavDrawer, //AppFooter,
            BottomNav,
            //ReturnBottle,


            //CustomerOrdersAdd,
            //CustomerOrdersView,
            //CustomerOrdersIndex,

            //CustomersAdd,
            //CustomersIndex,
            //CustomersView,

            //OwnerIndex,


        },
        props: {
            uid: String,
            appName: String,
            urlProfilePicture: String,
        },
        data() {
            return {
                menus: [
                    { to: '/', label: 'Dashboard', icon: 'fas fa-fw fa-tachometer-alt' },
                    { to: '/customers', label: 'Customers', icon: 'fas fa-fw fa-users' },
                    { to: '/customer-orders', label: 'Orders', icon: 'fas fa-fw fa-shopping-cart' },
                    //{ to: '/parents', label: 'Parents', icon: 'fas fa-fw fa-user' },
                    //{ to: '/clinic', label: 'Clinic', icon: 'fas fa-fw fa-clinic-medical' },
                    //{ to: '/staffs', label: 'Staffs', icon: 'fas fa-fw fa-user-nurse' },

                ],
                bottomNavHeightStyle: null
            }
        },
        async mounted() {

        },
        async created() {
            //const vm = this;

        },
        methods: {

        }
    }
</script>
