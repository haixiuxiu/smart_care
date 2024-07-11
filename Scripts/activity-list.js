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
        }
    }
});
