using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNApp.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace NNApp.Controllers
{
    [Authorize(Users = "vdoroshina@mlg.ru")]
    public class SubscriptionController : Controller
    {
        //Create database context
        SubscriptionContext cnt = new SubscriptionContext();

        //Create direct connection to database
        static string db = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
        SqlConnection cn = new SqlConnection(db);


        // GET: Subscription
        public ActionResult Index(string SearchString)
        {
            object items;

            //Create query string for filtering
            if (!String.IsNullOrEmpty(SearchString))
            {
                SearchString = SearchString.Trim();
                items = cnt.Subscriptions.Where(m => m.NewspaperName.Contains(SearchString) || m.email.Contains(SearchString));
            }
            else
            {
                items = from m in cnt.Subscriptions select m;
            }

            //return Content(str);
            return View(items);
        }

        // GET: Subscription/Details/5
        public ActionResult Details(int id)
        {
            var items = from m in cnt.Subscriptions where m.id == id select m;
            return View(items.First());
        }

        // GET: Subscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Subscription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscription/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subscription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult RemoveEmail(int SubscriptionId, string Email)
        {
            var item = cnt.Subscriptions.Single(m => m.id == SubscriptionId);
            var email_old = (string)item.email.Trim();

            //is email for remove in email_old?
            if (email_old.IndexOf(Email) == -1)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        Message = Email + " is not in old email string " + email_old
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Remove email
            string[] s2 = email_old.Split(';');

            string email_new = "";

            foreach (string s3 in s2)
            {
                string s4 = s3.Trim();

                if (s4 != "" && s4 != Email)
                {
                    email_new = email_new + s4 + ";";
                }
            }

            //Update email
            if (email_new.Length > 0)
            {
                email_new = email_new.Remove(email_new.Length - 1);

                item.email = email_new;
                cnt.SaveChanges();

                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Success",
                        SubscriptionId = SubscriptionId,
                        RemovedEmail = Email,
                        EmailOld = email_old,
                        EmailNew = email_new,
                        DbString = "UPDATE NewspaperMail SET email = '" + email_new + "' WHERE id=" + SubscriptionId
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                //Email is empty, let's disable subscription
            }
            else
            {
                //Update email and disable subscription
                item.email = "";
                item.Enabled = false;
                cnt.SaveChanges();

                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Success",
                        LastEmailWasDeleted = true,
                        Message = "Last email was deleted, let's disable subscription."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult AddEmail(int SubscriptionId, string Email)
        {
            //Trim string
            Email = Email.Trim();

            //Check if email is empty
            if (Email == "")
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        Message = "Input email string is empty"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }

            //Query to database
            var query = from s in cnt.Subscriptions where s.id == SubscriptionId select s;

            //Check if no subscription with this id
            if (query.Count() == 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        Message = "No subscription with id " + SubscriptionId
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Get first row
            var item = query.Single(m => m.id == SubscriptionId);
            var email_old = item.email;

            //Add email
            if (email_old == "")
            {
                item.email = Email;
                item.Enabled = true;
            }
            else
            {
                item.email += ";" + Email;
            }

            //Save to db
            cnt.SaveChanges();

            //Return result
            return new JsonResult()
            {
                Data = new
                {
                    Result = "Success",
                    email_old = email_old,
                    email_new = item.email,
                    enabled = Convert.ToInt32(item.Enabled)
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult ChangeActive(int SubscriptionId, int val)
        {
            //Query to database
            var query = from s in cnt.Subscriptions where s.id == SubscriptionId select s;

            //Check if no subscription with this id
            if (query.Count() == 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        Message = "No subscription with id " + SubscriptionId
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Check for val value
            if (val != 1 && val != 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        ErrorText = "Incorrect value of val"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }

            //Get first row
            var item = query.Single();

            //Check if no email in subscription
            if (item.email == "")
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        ErrorCode = "EmptyEmail"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }

            //Save new value
            item.Enabled = Convert.ToBoolean(val);
            cnt.SaveChanges();

            //Return result
            return new JsonResult()
            {
                Data = new
                {
                    Result = "Success",
                    new_active_value = val
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult SearchNewspaper(string Text)
        {
            if (String.IsNullOrEmpty(Text))
            {
                return new JsonResult()
                {

                    Data = new
                    {
                        Result = "Error",
                        ErrorCode = "EmptySearchString"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Query db
            string filter_query = " where n.NewspaperName like '%" + Text + "%'";
            SqlCommand cmd = new SqlCommand("SELECT top 7 * FROM Newspaper n left join NewspaperMail m on  n.newspaper_id = m.newspaper_id" + filter_query + " and m.enabled is null order by n.NewspaperName asc", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ArrayList myAL = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                myAL.Add(new { id = row["Newspaper_Id"], name = row["Newspapername"], enabled = row["enabled"] });
                //str += row["Newspapername"] + "<br/>";
            }

            return new JsonResult()
            {
                Data = new
                {
                    Result = "Success",
                    Data = myAL
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult CreateSubscr(int NPid, string NPname)
        {
            //Check if subscription with same newspaper id exists
            var query = from s in cnt.Subscriptions where s.newspaper_id == NPid select s;
            if (query.Count() > 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        ErrorCode = "SubscriptionWithSameNewspaperIdAlreadyExists"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Calculate id for new subscription
            int new_id = cnt.Subscriptions.Max(s => s.id) + 1;

            //Create object instance
            var new_subscr = new Subscription { id = new_id, newspaper_id = NPid, NewspaperName = NPname, email = "", MessageType = 5, FromEmail = "income5@mlg.ru", Enabled = false };

            //Add to database
            cnt.Subscriptions.Add(new_subscr);
            cnt.SaveChanges();

            //Return
            return new JsonResult()
            {
                Data = new
                {
                    Result = "Success",
                    CreatedItem = new_subscr
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SaveComment(int SubscriptionId, string Text)
        {
            //Query to database
            var query = from s in cnt.Subscriptions where s.id == SubscriptionId select s;

            //Check if no subscription with this id
            if (query.Count() == 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Result = "Error",
                        Message = "No subscription with id " + SubscriptionId
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //Get first row
            var item = query.Single();

            //Save new value
            item.Comment = Text.Trim();
            cnt.SaveChanges();

            //Return result
            return new JsonResult()
            {
                Data = new
                {
                    Result = "Success",
                    Code = "CommentSavedSuccessfully"
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
    }
}

