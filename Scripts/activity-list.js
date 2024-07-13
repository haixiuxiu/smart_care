new Vue({
    el: '#app',
    data: {
        activities: [],
        errorMessage: ''
    },
    created() {
        this.fetchActivities();
    },
    methods: {
        fetchActivities() {
            axios.get('/ActivityAudit/GetActivities')
                .then(response => {
                    if (response.data.errorMessage) {
                        this.errorMessage = response.data.errorMessage;
                    } else {
                        this.activities = response.data.activities;
                    }
                })
                .catch(error => {
                    console.error('Error fetching activities:', error);
                    this.errorMessage = '无法获取活动数据。';
                });
        },
        handleApproval(activityId) {
            const activityIndex = this.activities.findIndex(a => a.EVENT_ID === activityId);
            if (activityIndex !== -1) {
                // 从activities数组中移除已审核的活动
                this.activities.splice(activityIndex, 1);
            }
        },
        handleRejection(activityId) {
            const activityIndex = this.activities.findIndex(a => a.EVENT_ID === activityId);
            if (activityIndex !== -1) {
                // 从activities数组中移除已驳回的活动
                this.activities.splice(activityIndex, 1);
            }
        }
    }
});
