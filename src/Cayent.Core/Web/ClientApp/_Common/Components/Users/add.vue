<style></style>
<template>
    <div v-cloak>

        <div class="d-sm-flex align-items-center justify-content-between">
            <h1 class="h3">
                <i class="fas fa-fw fa-user mr-1"></i>Add User
            </h1>
            <div class="text-right">
                <button @click="save" class="btn btn-primary">
                    Save
                </button>
                <button @click="close" class="btn btn-secondary">
                    Cancel
                </button>
            </div>
        </div>
        <div class="mt-2">
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">First Name</label>
                <div class="col">
                    <input v-model="item.firstName" type="text" class="form-control" v-bind:class="{'is-invalid':validations.get('firstName')}" />
                    <span v-if="validations.get('firstName')" class="text-danger">
                        {{validations.get('firstName')}}
                    </span>
                </div>
            </div>
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Last Name</label>
                <div class="col">
                    <input v-model="item.lastName" type="text" class="form-control" v-bind:class="{'is-invalid':validations.get('lastName')}" />
                    <span v-if="validations.get('lastName')" class="text-danger">
                        {{validations.get('lastName')}}
                    </span>
                </div>
            </div>
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Email</label>
                <div class="col">
                    <input v-model="item.email" type="email" class="form-control" v-bind:class="{'is-invalid':validations.get('email')}" />
                    <span v-if="validations.get('email')" class="text-danger">
                        {{validations.get('email')}}
                    </span>
                </div>
            </div>

            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Phone Number</label>
                <div class="col-sm-6">
                    <input v-model="item.phoneNumber" type="tel" class="form-control" v-bind:class="{'is-invalid':validations.get('phoneNumber')}" />
                    <span v-if="validations.get('phoneNumber')" class="text-danger">
                        {{validations.get('phoneNumber')}}
                    </span>
                </div>
            </div>
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Password</label>
                <div class="col-sm-6">
                    <input v-model="item.password" type="password" class="form-control" v-bind:class="{'is-invalid':validations.get('password')}" />
                    <span v-if="validations.get('password')" class="text-danger">
                        {{validations.get('password')}}
                    </span>
                </div>
            </div>
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Confirm Password</label>
                <div class="col-sm-6">
                    <input v-model="item.confirmPassword" type="password" class="form-control" v-bind:class="{'is-invalid':validations.get('confirmPassword')}" />
                    <span v-if="validations.get('confirmPassword')" class="text-danger">
                        {{validations.get('confirmPassword')}}
                    </span>
                </div>
            </div>
            <div class="form-group form-row">
                <label class="col-sm-3 text-sm-right col-form-label">Role</label>
                <div class="col-sm-6">
                    <b-form-select v-model="item.roleId" :options="roles" v-bind:class="{'is-invalid':validations.get('roleId')}"></b-form-select>
                    <span v-if="validations.get('confirmPassword')" class="text-danger">
                        {{validations.get('roleId')}}
                    </span>
                </div>
            </div>

        </div>
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
            urlView: String
        },
        data() {
            return {
                validations: new Map(),
                item: {
                    firstName: '',
                    lastName: '',
                    email: '',
                    phoneNumber: '',
                    password: '',
                    confirmPassword: '',
                    roleId: null
                },
                roles: [
                    { value: 'assistant', text: 'Assistant' },
                    { value: 'frontliner', text: 'Front Liner' },
                    { value: 'driver', text: 'Driver' }
                ]
            };
        },
        async created() {
            const vm = this;

        },
        methods: {

            isFormValid() {
                const vm = this;
                const item = vm.item;

                const validations = new Map();

                if (!item.firstName)
                    validations.set('firstName', 'First Name is required.');
                if (!item.lastName)
                    validations.set('lastName', 'Last Name is required.');
                if (!item.email)
                    validations.set('email', 'Email is required.');
                if (!item.phoneNumber)
                    validations.set('phoneNumber', 'Phone Number is required.');
                if (!item.password)
                    validations.set('password', 'Password is required.');
                if (!item.confirmPassword)
                    validations.set('confirmPassword', 'Confirm Password is required.');
                if (item.password != item.confirmPassword)
                    validations.set('confirmPassword', 'Please confirm password.');
                if (!item.roleId)
                    validations.set('roleId', 'Role is required.');

                vm.validations = validations;

                return validations.size == 0;
            },
            async save() {
                const vm = this;

                if (!vm.isFormValid())
                    return

                if (vm.busy)
                    return;

                try {
                    const payload = vm.item;

                    await vm.$util.axios.post(`api/manager/users`, payload)
                        .then(resp => {
                            vm.$bvToast.toast('User created', { title: 'Create User', variant: 'success', toaster: 'b-toaster-bottom-right'  });

                            setTimeout(() => {
                                const url = `${vm.urlView}${resp.data}?r=${vm.rurl}`;

                                vm.$util.href(url);
                            }, 500);
                        })
                } catch (e) {
                    vm.$util.handleError(e);
                }
            }

        }
    }
</script>