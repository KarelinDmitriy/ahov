﻿using System;
using System.Web;

namespace Web.Core
{
	public static class HttpApplicationHelper
	{
		public static string GetCookie(this HttpApplication app, string name)
		{
			var time = app.Request.Cookies.Get(CookiesConst.Time);
			if (time == null)
				return null;
			DateTime exp;
			if (!DateTime.TryParse(time.Value, out exp))
				return null;
			if (exp < DateTime.UtcNow)
				return null;
			var httpCookie = app.Request.Cookies.Get(name);
			return httpCookie?.Value;
		}
	}
}