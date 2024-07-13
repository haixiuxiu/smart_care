Vue.component('comment-item', {
    props: ['comment'],
    template: `
        <div class="comment">
            <p><strong>{{ comment.USER_ID }}</strong>: {{ comment.COMMENT_CONTENT }}</p>
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
        }
    }
});
