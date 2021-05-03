<template>
    <b-navbar fixed="bottom" type="dark" variant="dark" class="d-block d-sm-none">
        <b-navbar-nav align="center" :justified="true">
            <template v-for="menu in menus">

                <b-nav-item v-if="!menu.subMenus" :to="menu.to">
                    <span v-bind:class="menu.icon"></span>
                </b-nav-item>
                <b-nav-item-dropdown v-else dropup dropleft>
                    <template v-slot:button-content>
                        <span v-bind:class="menu.icon"></span>
                    </template>
                    <b-dropdown-item v-for="mnu in menu.subMenus" :to="mnu.to" :key="mnu.to">
                        <span class="mr-1" v-bind:class="mnu.icon"></span>{{mnu.label}}
                    </b-dropdown-item>

                </b-nav-item-dropdown>

            </template>
        </b-navbar-nav>
    </b-navbar>
</template>
<script>
    import pageMixin from '../../../_Core/Mixins/pageMixin';

    export default {
        mixins: [pageMixin],

        props: {
            uid: String,
            menus: Array
        },

        data() {
            return {

            };
        },

        computed: {

        },

        async created() {
            const vm = this;

        },

        async mounted() {
            const vm = this;

        },

        methods: {

        }
    }
</script>