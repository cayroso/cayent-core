'use strict';

import VueRouter from 'vue-router';

import index from './Pages/index.vue';

import accountsIndex from './Pages/Accounts/index.vue';

import customersAdd from './Pages/Customers/add.vue';
import customersIndex from './Pages/Customers/index.vue';
import customersView from './Pages/Customers/view.vue';

import customerOrdersAdd from './Pages/CustomerOrders/Add/index.vue';
import customerOrdersIndex from './Pages/CustomerOrders/index.vue';
import customerOrdersView from './Pages/CustomerOrders/View/index.vue';

////import appointmentsAdd from './Pages/Appointments/add.vue';
////import appointmentsView from './Pages/Appointments/index.vue';

//import childrenIndex from './Pages/Children/index.vue';
//import childrenView from './Pages/Children/view.vue';

//import parentsIndex from './Pages/Parents/index.vue';
//import parentsView from './Pages/Parents/view.vue';

//import staffsIndex from './Pages/Staffs/index.vue';
//import staffsAdd from './Pages/Staffs/add.vue';
//import staffsView from './Pages/Staffs/view.vue';

//import clinicIndex from './Pages/Clinic/index.vue';

const NotFound = {
    template: '<div>Not found</div>'
};

const routes = [
    { path: '/', name: "index", component: index },

    { path: '/accounts', name: "accountsIndex", component: accountsIndex },

    ////{ path: '/contacts', name: "contacts", component: contactsIndex },

    { path: '/customers/add', name: "customersAdd", component: customersAdd },
    { path: '/customers', name: "customersIndex", component: customersIndex },
    { path: '/customers/view/:id', name: "customersView", component: customersView, props: true },

    { path: '/customer-orders/add', name: "customerOrdersAdd", component: customerOrdersAdd },
    { path: '/customer-orders', name: "customerOrdersIndex", component: customerOrdersIndex },
    { path: '/customer-orders/view/:id', name: "customerOrdersView", component: customerOrdersView, props: true },

    //{ path: '/children', name: "childrenIndex", component: childrenIndex },
    //{ path: '/children/view/:id', name: "childrenView", component: childrenView, props: true },

    //{ path: '/parents', name: "parentsIndex", component: parentsIndex },
    //{ path: '/parents/view/:id', name: "parentsView", component: parentsView, props: true },

    //{ path: '/clinic', name: "clinicIndex", component: clinicIndex },

    //{ path: '/staffs', name: "staffsIndex", component: staffsIndex },
    //{ path: '/staffs/add', name: "staffsAdd", component: staffsAdd },
    //{ path: '/staffs/view/:id', name: "staffsView", component: staffsView, props: true },

    { path: '*', component: NotFound },
];

const router = new VueRouter({
    base: '/owner',
    mode: "history",
    routes: routes,
});

export default router;
