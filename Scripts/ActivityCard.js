Vue.component('activity-card', {
    props: ['activity'],
    template: `
        <div class="card">
            <div class="card-header">
                {{ activity.EVENT_NAME }}
            </div>
            <div class="card-body">
                <h5 class="card-title">ID: {{ activity.EVENT_ID }}</h5>
                <p class="card-text">时间: {{ activity.EVENT_DATE }}</p>
                
                <a v-on:click.prevent="approveActivity(activity.EVENT_ID)" class="btn btn-success btn-sm">审核通过</a>
                
                <a :href="'/ActivityDetail/index/' + activity.EVENT_ID" class="btn btn-info btn-sm">详情</a>

                <a v-on:click.prevent="rejectActivity(activity.EVENT_ID)" class="btn btn-danger btn-sm">驳回</a>
            </div>
        </div>
    `,
    methods: {
        approveActivity(eventId) {
            axios.post(`/ActivityAudit/ApproveActivities`, {
                eventId: eventId
                }
            )
                .then(response => {
                    if (response.data.success) {
                        this.activity.EVENT_STATE = '审核通过';
                        this.$emit('approved', eventId);
                    } else {
                        alert('审核失败');
                    }
                })
                .catch(error => {
                    console.error('审核请求失败:', error);
                    alert('审核请求失败');
                });
        },
        rejectActivity(eventId) {
            axios.post(`/ActivityAudit/RejectActivity`, {
                eventId: eventId
            })
                .then(response => {
                    if (response.data.success) {
                        this.activity.EVENT_STATE = '已驳回';
                        this.$emit('rejected', eventId);
                    } else {
                        alert('驳回失败');
                    }
                })
                .catch(error => {
                    console.error('驳回请求失败:', error);
                    alert('驳回请求失败');
                });
        }
    }

});

