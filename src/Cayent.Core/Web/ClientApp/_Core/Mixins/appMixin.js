
'use strict';

import * as signalR from '@microsoft/signalr';

export default {
    components: {
    },
    data() {
        return {
            enums: {
                orderStatus: {
                    requested: 1,
                    placed: 2,
                    accepted: 3,
                    processed: 4,
                    delivered: 5,
                    completed: 6,
                    cancelled: 7
                },
            },
            bottomNavHeightStyle: null,
            messageIds: []
        };
    },
    async created() {
        const vm = this;


        //vm.$bus.$on('event:open-notification', vm.onOpenNotification);
        //vm.$bus.$on('event:close-notification', vm.onCloseNotification);

        //vm.$bus.$on('event:open-chat', vm.onOpenChat);
        //vm.$bus.$on('event:close-chat', vm.onCloseChat);
        //vm.$bus.$on('event:send-message', vm.onSendMessage);

        //vm.$bus.$on('event:quick-view-account', vm.onQuickViewAccount);

        //vm.$bus.$on('event:quick-view-qb-transaction', vm.onQuickViewQbTransaction);

        //await Promise.all([vm.connectOrderHub()]);
    },
    async mounted() {
        const vm = this;

        vm.getBottomNavHeight();

        if (vm.uid) {
            //await vm.connectNotificationHub();

            //await vm.connectChatHub();

            //await vm.connectAppointmentHub();

            //vm.$bus.$on('event:open-chat', vm.onOpenChat);
            //vm.$bus.$on('event:close-chat', vm.onCloseChat);
            //vm.$bus.$on('event:send-message', vm.onSendMessage);
            //vm.$bus.$on('event:send-team-message', vm.onSendTeamMessage);
        }
    },
    methods: {
        getBottomNavHeight() {
            const vm = this;
            const bottomNav = vm.$refs.bottomNav;
            let bottomNavHeight = bottomNav.$el.clientHeight;

            if (bottomNavHeight > 0) {
                vm.bottomNavHeightStyle = `padding-bottom:${bottomNavHeight + 10}px;`;
            }
        },

        scrollIntoView() {
            const topElem = document.getElementById('top');
            if (topElem) {
                topElem.scrollIntoView({ behavior: 'smooth' });
                //debugger
            }
        },

        showHiddenElements() {
            let vm = this;

            if (!vm.$el.getElementsByClassName)
                return;

            let elems = vm.$el.getElementsByClassName('initialHidden');

            for (let i = 0; i < elems.length; i++) {
                let el = elems[i];

                el.classList.remove('invisible');
                el.classList.remove('d-none');
            }

        },
        close() {
            //const vm = this;

            //const href = atob(vm.rurl) || vm.durl;
            //vm.$util.href(href);
            const referrer = document.referrer;

            if (referrer && referrer.includes('/Identity/Account/Login')) {
                history.go(-2);
            }
            else {
                history.back();
            }
        },

        async connectNotificationHub() {
            const vm = this;

            const notificationHub = new signalR.HubConnectionBuilder()
                .withUrl('/notificationHub')
                //.configureLogging(signalR.LogLevel.Debug)
                .withAutomaticReconnect()
                .build();

            notificationHub.on('received', function (resp) {

                const message = resp.content || "-no reason specified-";

                const h = vm.$createElement;
                // Create the message
                let foo = [
                    h('span', message),
                    h('p'),
                ];

                if (resp.refLink) {
                    //debugger
                    foo.push(
                        h('a', { attrs: { href: resp.refLink } }, 'Click here to View.')
                    )
                }
                //debugger
                const vNodesMsg = h(
                    'div', foo
                );

                var route = window.location;
                var contains = route.pathname.includes(resp.itemId);

                if (!contains || (contains && resp.itemEvent !== 'Post:Comment')) {
                    vm.$bvToast.toast([vNodesMsg], {
                        title: `${resp.subject}`,
                        variant: 'info',
                        solid: true
                    });
                }

                //  emit event
                vm.$bus.$emit('event:notification-received', resp);
            });

            notificationHub.on('jobUpdated', function (resp) {
                vm.$bus.$emit('event:job-updated', resp);
            });

            await notificationHub.start().then(function () {
                //debugger;
            }).catch(function (err) {
                //debugger;
                //vm.$util.handleError(err);
            });
        },
    }
}