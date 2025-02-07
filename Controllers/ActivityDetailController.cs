﻿using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ActivityDetailController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetActivityDetails(string id)
        {
            if (int.TryParse(id, out int activityId))
            {
                var activity = db.EVENT.FirstOrDefault(e => e.EVENT_ID == activityId);
                if (activity == null)
                {
                    return Json(new { errorMessage = "活动不存在。" }, JsonRequestBehavior.AllowGet);
                }

                var formattedActivity = new
                {
                    activity.EVENT_ID,
                    activity.EVENT_NAME,
                    activity.EVENT_TYPE,
                    activity.EVENT_CONTENT,
                    activity.EVENT_SITE,
                    EVENT_DATE = activity.EVENT_DATE.ToString("yyyy-MM-dd"),
                    EVENT_START_TIME = activity.EVENT_START_TIME.ToString("HH:mm:ss"),
                    EVENT_END_TIME = activity.EVENT_END_TIME.ToString("HH:mm:ss"),
                    activity.STAR_NUMBER,
                    activity.PARTICIPANT_NUMBER,
                    activity.INITIATOR_ID
                };

                return Json(new { activity = formattedActivity }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errorMessage = "无效的活动ID。" }, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult GetComments(string id)
        {
            if (int.TryParse(id, out int activityId))
            {
                var comments = db.DISCUSSION
                .Where(c => c.ACTIVITY_ID == activityId)
                .OrderBy(c => c.COMMENT_TIME) // 按时间由小到大排序
                .AsEnumerable() // 将查询转换为内存中的列表
                .Select(c => new
                {
                    c.USER_ID,
                    c.COMMENT_CONTENT,
                    COMMENT_TIME = c.COMMENT_TIME.ToString("yyyy-MM-dd HH:mm"), // 转换为字符串并指定格式
                    c.COMMENT_ID,
                    c.PARENT_ID
                })
                .ToList();
                return Json(new { comments }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errorMessage = "无效的活动ID。" }, JsonRequestBehavior.AllowGet);
        }

        public static int GenerateCommentId()
        {
            // 获取当前的时间戳（秒）
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            // 生成一个随机数
            int random = new Random().Next(1000);
            // 组合时间戳和随机数，并将其模 int.MaxValue 以确保其值在 int32 范围内
            int commentId = (int)((timestamp * 1000 + random) % int.MaxValue);
            return commentId;
        }

        [HttpPost]
        public JsonResult SubmitComment(int activityId, string content, int? parentId = null)
        {
            // 添加日志
            System.Diagnostics.Debug.WriteLine("SubmitComment called with activityId: " + activityId + ", parentId: " + parentId + " and comment: " + content);
            try
            {
                var discussion = new DISCUSSION
                {
                    COMMENT_ID = GenerateCommentId(),
                    USER_ID = 10000,
                    ACTIVITY_ID = activityId,
                    COMMENT_CONTENT = content,
                    COMMENT_TIME = DateTime.Now,
                    LIKES_NUMBER = 0,
                    PARENT_ID = parentId // Assuming DISCUSSION has a PARENT_ID field
                };
                db.DISCUSSION.Add(discussion);
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    comment = new
                    {
                        discussion.ACTIVITY_ID,
                        discussion.USER_ID,
                        discussion.COMMENT_ID,
                        discussion.COMMENT_CONTENT,
                        discussion.PARENT_ID,
                        COMMENT_TIME = discussion.COMMENT_TIME.ToString("yyyy-MM-dd HH:mm")
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "无法提交评论。" + ex.Message });
            }
        }
    }
}
