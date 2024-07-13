Vue.component('comment-item', {
    props: ['comment'],
    template: `
                <div class="comment">
                    <p><strong>{{ comment.USER_ID }}</strong>: {{ comment.COMMENT_CONTENT }}</p>
                    <p><em>{{ formatDate(comment.COMMENT_TIME) }}</em></p>
                    <button @click="replying = !replying" class="btn btn-link">回复</button>
                    <div v-if="replying">
                        <textarea v-model="replyContent" class="form-control" rows="2"></textarea>
                        <button @click="submitReply" class="btn btn-primary mt-2">提交回复</button>
                    </div>
                    <div class="replies" v-if="comment.replies && comment.replies.length">
                        <comment-item v-for="reply in comment.replies" :key="reply.COMMENT_ID" :comment="reply" @reply="$emit('reply', $event)"></comment-item>
                    </div>
                </div>
            `,
    data() {
        return {
            replying: false,
            replyContent: ''
        };
    },
    methods: {
        submitReply() {
            if (this.replyContent.trim()) {
                const parentId = this.comment.COMMENT_ID;
                this.$emit('reply', { parentId: parentId, content: this.replyContent });
                this.replyContent = '';
                this.replying = false;
            }
        },
        formatDate(date) {
            if (!date) return '无效日期';
            const parsedDate = new Date(date);
            return isNaN(parsedDate.getTime()) ? '无效日期' : parsedDate.toISOString().split('T')[0];
        }
    }
});

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
                        this.comments = this.buildCommentTree(response.data.comments);
                    }
                })
                .catch(error => {
                    console.error('Error fetching comments:', error);
                    this.errorMessage = '无法获取评论。';
                });
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
                        this.comments.push({
                            COMMENT_ID: newComment.COMMENT_ID,
                            USER_ID: newComment.USER_ID,
                            COMMENT_CONTENT: newComment.COMMENT_CONTENT,
                            COMMENT_TIME: newComment.COMMENT_TIME,
                            PARENT_ID: newComment.PARENT_ID,
                            replies: []
                        });
                        this.newComment = '';
                    } else {
                        alert('提交评论失败!: ' + response.data.errorMessage);
                    }
                })
                .catch(error => {
                    console.error('Error submitting comment:', error);
                    alert('提交评论失败，请稍后再试');
                });
        },
        handleReply({ parentId, content }) {
            const activityId = window.location.pathname.split('/').pop();
            axios.post('/ActivityDetail/SubmitComment', {
                activityId: activityId,
                content: content,
                parentId: parentId
            })
                .then(response => {
                    if (response.data.success) {
                        const reply = response.data.comment;
                        const parentComment = this.findCommentById(this.comments, parentId);
                        if (parentComment) {
                            parentComment.replies.push({
                                COMMENT_ID: reply.COMMENT_ID,
                                USER_ID: reply.USER_ID,
                                COMMENT_CONTENT: reply.COMMENT_CONTENT,
                                PARENT_ID: reply.PARENT_ID,
                                COMMENT_TIME: reply.COMMENT_TIME,
                                replies: []
                            });
                        }
                    } else {
                        alert('回复失败!: ' + response.data.errorMessage);
                    }
                })
                .catch(error => {
                    console.error('Error submitting reply:', error);
                    alert('回复失败，请稍后再试');
                });
        },
        findCommentById(comments, id) {
            for (const comment of comments) {
                if (comment.COMMENT_ID === id) {
                    return comment;
                } else if (comment.replies.length) {
                    const found = this.findCommentById(comment.replies, id);
                    if (found) {
                        return found;
                    }
                }
            }
            return null;
        },
        buildCommentTree(comments) {
            const map = {};
            const roots = [];

            comments.forEach(comment => {
                comment.replies = [];
                map[comment.COMMENT_ID] = comment;
                if (comment.PARENT_ID) {
                    if (map[comment.PARENT_ID]) {
                        map[comment.PARENT_ID].replies.push(comment);
                    }
                } else {
                    roots.push(comment);
                }
            });

            return roots;
        },
        formatDate(date) {
            if (!date) return '无效日期';
            const parsedDate = new Date(date);
            return isNaN(parsedDate.getTime()) ? '无效日期' : parsedDate.toISOString().split('T')[0];
        },
        formatTime(time) {
            if (!time) return '无效时间';
            const parsedTime = new Date('1970-01-01T' + time); // 添加日期部分
            return isNaN(parsedTime.getTime()) ? '无效时间' : parsedTime.toISOString().split('T')[1].substring(0, 5);
        }
    }
});