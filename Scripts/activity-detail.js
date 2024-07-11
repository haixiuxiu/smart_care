new Vue({
    el: '#app',
    delimiters: ['[[', ']]'],
    data: {
        activity: {},
        comments: [],
        newComment: '',
        errorMessage: ''
    },
    created() {
        const activityId = window.location.pathname.split('/').pop();
        this.fetchActivityDetails(activityId);
        this.fetchComments(activityId);
    },
    methods: {
        fetchActivityDetails(activityId) {
            axios.get(`/ActivityDetail/GetActivityDetails?id=${activityId}`)
                .then(response => {
                    if (response.data.errorMessage) {
                        this.errorMessage = response.data.errorMessage;
                    } else {
                        this.activity = response.data.activity;
                        console.log('activity-detail.js loaded');
                    }
                })
                .catch(error => {
                    console.error('Error fetching activity:', error);
                    this.errorMessage = '无法获取活动详细信息。';
                });
        },
        fetchComments(activityId) {
            axios.get(`/ActivityDetail/GetComments?id=${activityId}`)
                .then(response => {
                    if (response.data.errorMessage) {
                        this.errorMessage = response.data.errorMessage;
                    } else {
                        this.comments = response.data.comments;
                    }
                })
                .catch(error => {
                    console.error('Error fetching comments:', error);
                    this.errorMessage = '无法获取评论。';
                });
        },
        formatDate(date) {
            if (!date) return '无效日期';
            const parsedDate = new Date(date + 'T00:00:00'); // 添加时间部分
            return isNaN(parsedDate.getTime()) ? '无效日期' : parsedDate.toISOString().split('T')[0];
        },
        formatTime(time) {
            if (!time) return '无效时间';
            const parsedTime = new Date('1970-01-01T' + time); // 添加日期部分
            return isNaN(parsedTime.getTime()) ? '无效时间' : parsedTime.toISOString().split('T')[1].substring(0, 5);
        },
        submitComment() {
            if (this.newComment.trim() === '') {
                alert('评论内容不能为空');
                return;
            }

            const activityId = window.location.pathname.split('/').pop();
            axios.post('/ActivityDetail/SubmitComment', {
                activityId: activityId,
                content: this.newComment
            })
                .then(response => {
                    if (response.data.success) {
                        const newComment = response.data.comment;
                        //newComment.COMMENT_TIME = this.formatDate(newComment.COMMENT_TIME) + ' ' + this.formatTime(newComment.COMMENT_TIME);
                        this.comments.push(newComment);
                        this.newComment = '';
                    } else {
                        alert('提交评论失败!: ' + response.data.errorMessage);
                    }
                })
                .catch(error => {
                    console.error('Error submitting comment:', error);
                    alert('提交评论失败，请稍后再试');
                });
        }
    }
});
