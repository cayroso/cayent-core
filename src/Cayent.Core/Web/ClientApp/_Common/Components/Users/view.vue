<style></style>
<template>
    <div v-cloak>
        <div class="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center">
            <div>
                <h1 class="h3 mb-sm-0">
                    <i class="fas fa-fw fa-user-shield mr-1"></i>View User
                </h1>
            </div>

            <div class="text-right">
                <button @click="openAssignRole" class="btn btn-outline-primary">
                    <span class="fas fa-fw fa-plus mr-1"></span>Role
                </button>
                <div class="btn-group">
                    <button type="button" class="btn btn-outline-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="fas fa-fw fa-eye"></span>Task
                    </button>
                    <div class="dropdown-menu dropdown-menu-right">
                        <h6 class="dropdown-header">This Week</h6>
                        <button @click="viewThisWeekTodo" class="dropdown-item" type="button">Todo</button>
                        <button @click="viewThisWeekInProgress" class="dropdown-item" type="button">In Progress</button>
                        <button @click="viewThisWeekDone" class="dropdown-item" type="button">Done</button>
                        <div class="dropdown-divider"></div>
                        <button @click="viewAll" class="dropdown-item" type="button">All</button>
                    </div>
                </div>


                <button @click="get" type="button" class="btn btn-primary ml-1">
                    <span class="fas fa-fw fa-sync"></span>
                </button>
                <button @click="close" class="btn btn-secondary">
                    <span class="fas fa-fw fa-times-circle"></span>
                </button>

            </div>
        </div>

        <b-overlay :show="busy">
            <div class="mt-2">
                <div class="form-group row">
                    <label class="col-sm-4 text-sm-right col-form-label">First Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{item.firstName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-sm-right col-form-label">Last Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{item.lastName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-sm-right col-form-label">Email</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{item.email}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-sm-right col-form-label">Phone Number</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{item.phoneNumber}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-sm-right col-form-label">
                        Role(s)
                    </label>
                    <div class="col-sm">
                        <div class="table-responsive">
                            <table class="table table-bordered">
                                <thead>
                                    <tr class="bg-success">
                                        <td>#</td>
                                        <td>Name</td>
                                        <td class="m-1 p-1"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(r,i) in item.roles" :key="r.roleId">
                                        <td>{{i+1}}</td>
                                        <td>{{r.name}}</td>
                                        <td class="text-center align-middle m-1 p-1">
                                            <button @click="removeRole(r)" class="btn btn-sm btn-outline-primary">
                                                <span class="fas fa-fw fa-times-circle"></span>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </b-overlay>

        <b-modal id="modalViewTasks"
                 :no-close-on-esc="true"
                 :no-close-on-backdrop="true"
                 body-class="p-2"
                 size="lg">
            <template v-slot:modal-header>
                <div class="d-sm-flex align-items-center justify-content-between">
                    <h1 class="h5 m-0">Tasks</h1>
                </div>
            </template>
            <template v-slot:modal-footer>
                <div class="d-flex flex-row justify-content-between">
                    <button @click="$bvModal.hide('modalViewTasks')" class="btn btn-secondary ml-1">
                        Close
                    </button>
                </div>
            </template>
            <div class="table-responsive mb-0">
                <table class="table table-bordered m-0">
                    <thead>
                        <tr class="bg-success">
                            <td>#</td>
                            <td>Status</td>
                            <td>Dates</td>
                            <td>Title</td>
                            <td></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,i) in tasks.items">
                            <td>{{i+1}}</td>
                            <td>{{item.statusText}}</td>
                            <td>
                                <div class="form-group form-row mb-sm-0">
                                    <label class="col-md-3 text-md-right col-form-label">Created</label>
                                    <div class="col-md">
                                        <span class="form-control-plaintext">{{item.dateCreated|moment('calendar')}}</span>
                                    </div>
                                </div>
                                <div class="form-group form-row mb-sm-0">
                                    <label class="col-md-3 text-md-right col-form-label">Assigned</label>
                                    <div class="col-md">
                                        <span class="form-control-plaintext">{{item.dateAssigned}}</span>
                                    </div>
                                </div>
                                <div class="form-group form-row mb-sm-0">
                                    <label class="col-md-3 text-md-right col-form-label">Completed</label>
                                    <div class="col-md">
                                        <span class="form-control-plaintext">{{item.dateCompleted}}</span>
                                    </div>
                                </div>
                            </td>

                            <td>{{item.title}}</td>
                            <td>
                                <button @click="viewOrder(item.orderId)" class="btn btn-sm btn-outline-primary">
                                    <span class="fas fa-fw fa-eye"></span>
                                </button>
                            </td>

                        </tr>
                    </tbody>
                </table>
            </div>
        </b-modal>

        <b-modal id="modalAssignRole"
                 :no-close-on-esc="false"
                 :no-close-on-backdrop="true"
                 scrollable>
            <template v-slot:modal-header>
                <div class="d-sm-flex align-items-center justify-content-between">
                    <h1 class="h5 m-0">Assign Role</h1>
                </div>
            </template>
            <template v-slot:modal-footer>
                <button @click="assignRole" class="btn btn-primary">
                    Save
                </button>
                <button @click="closeAssignRole" class="btn btn-secondary">
                    Close
                </button>
            </template>
            <div>
                <b-form-select v-model="selectedRoleId" :options="roles"></b-form-select>
            </div>
        </b-modal>

    </div>
</template>
<script>
    import pageMixin from '../../Mixins/pageMixin';
    export default {
        mixins: [pageMixin],
        props: {
            uid: String,
            durl: String,
            rurl: String,
            id: String,
            urlView: String
        },
        data() {
            return {
                item: {},
                tasks: [],

                selectedRoleId: null,
                roles: [],
            };
        },
        async created() {
            const vm = this;

            await vm.get();
        },
        methods: {
            async getLookups() {
                const vm = this;

                try {
                    await vm.$util.axios.get(`api/lookups/roles`)
                        .then(resp => {

                            const roleIds = vm.item.roles.map(e => e.roleId);
                            vm.roles = resp.data;
                            vm.roles.forEach(e => {
                                e.disabled = roleIds.includes(e.value);
                            });
                        });
                } catch (e) {
                    vm.$util.handleError(e);
                }
            },
            async get() {
                const vm = this;

                try {
                    await vm.$util.axios.get(`api/manager/users/${vm.id}`)
                        .then(resp => {
                            vm.item = resp.data;
                        });
                } catch (e) {
                    vm.$util.handleError(e);
                }
            },

            viewOrder(orderId) {
                const vm = this;
                const rUrl = btoa(window.location.href);
                const url = `${vm.urlView}${orderId}/?r=${rUrl}`;

                vm.$util.href(url);
            },

            viewThisWeekTodo() {
                const vm = this;
                var start = moment().startOf('week');
                var end = moment().endOf('week');

                vm.viewTasks(1, start, end)
            },
            viewThisWeekInProgress() {
                const vm = this;
                var start = moment().startOf('week');
                var end = moment().endOf('week');

                vm.viewTasks(2, start, end)
            },
            viewThisWeekDone() {
                const vm = this;
                var start = moment().startOf('week');
                var end = moment().endOf('week');

                vm.viewTasks(3, start, end)
            },
            viewAll() {
                const vm = this;
                var start = moment().startOf('month');
                var end = moment().endOf('month');

                vm.viewTasks(0, start, end);
            },

            async viewTasks(status, start, end) {
                const vm = this;


                if (vm.busy)
                    return;


                try {
                    vm.busy = true;

                    const query = [
                        '?status=', status,
                        '&sd=', start.valueOf(),
                        '&ed=', end.add(23, 'hours').add(59, 'minutes').valueOf()
                    ].join('');

                    await vm.$util.axios.get(`api/manager/users/${vm.id}/tasks/${query}`)
                        .then(resp => {
                            vm.tasks = resp.data;

                            vm.tasks.items.forEach(item => {
                                var dc = moment(item.dateCompleted);
                                var da = moment(item.dateAssigned);

                                if (dc.years() == 10000)
                                    item.dateCompleted = '';
                                else {
                                    item.dateCompleted = dc.calendar();
                                }

                                if (da.years() == 10000)
                                    item.dateAssigned = '';
                                else {
                                    item.dateAssigned = da.calendar();
                                }


                            });
                            vm.$bvModal.show('modalViewTasks');
                        })

                } catch (e) {
                    vm.$util.handleError(e);
                } finally {
                    vm.busy = false;
                }
            },

            async openAssignRole() {
                const vm = this;

                vm.selectedRoleId = null;
                await vm.getLookups();

                vm.$bvModal.show('modalAssignRole');
            },
            async assignRole() {
                const vm = this;

                try {

                    vm.busy = true;

                    const payload = {
                        userId: vm.item.userId,
                        roleId: vm.selectedRoleId,
                        token: vm.item.token
                    };

                    await vm.$util.axios.post(`api/manager/users/assign-role`, payload)
                        .then(resp => {
                            vm.$bvToast.toast('Role assigned.', { title: 'Assign Role', variant: 'success', toaster: 'b-toaster-bottom-right' });

                            vm.closeAssignRole();
                        });

                } catch (e) {
                    vm.$util.handleError(e);
                } finally {
                    vm.busy = false;

                    vm.get();
                }
            },
            closeAssignRole() {
                const vm = this;

                vm.$bvModal.hide('modalAssignRole');
            },
            async removeRole(role) {
                const vm = this;

                vm.$bvModal.msgBoxConfirm(`Are you sure you want to remove this role : ${role.name}?`)
                    .then(async value => {
                        if (value) {
                            try {
                                await vm.getLookups();

                                vm.busy = true;

                                const payload = {
                                    userId: vm.item.userId,
                                    roleId: role.roleId,
                                    token: vm.item.token
                                };

                                await vm.$util.axios.post(`api/manager/users/unassign-role`, payload)
                                    .then(resp => {
                                        vm.$bvToast.toast('Role removed.', { title: 'Unassign Role', variant: 'warning', toaster: 'b-toaster-bottom-right' });

                                        vm.get();
                                    });

                            } catch (e) {
                                vm.$util.handleError(e);
                            } finally {
                                vm.busy = false;
                            }
                        }
                    }).catch(vm.$util.handleError);
            },

        }
    }
</script>