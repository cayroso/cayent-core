'use strict';

import '../_Core/core';
import './main.css';

import Vue from 'vue';
import App from './Pages/_Shared/app.vue';

import VueObserveVisibility from 'vue-observe-visibility'
Vue.use(VueObserveVisibility);

import common from '../_Core/Plugins/common';
Vue.use(common);

import VueRouter from 'vue-router';
Vue.use(VueRouter);

import Router from './router';

new Vue({
    el: '#app',
    router: Router,
    components: {
        App
    },
    created() {
        $(document).ready(function () {
            $('.main').addClass('main-loaded');
        });
    }
});