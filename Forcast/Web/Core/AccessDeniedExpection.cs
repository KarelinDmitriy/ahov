using System;

namespace Web.Core
{
	public class AccessDeniedExpection : Exception
	{
		public AccessDeniedExpection(string reason)
			: base(reason)
		{
			
		}
	}
}