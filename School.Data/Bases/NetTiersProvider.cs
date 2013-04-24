#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using School.Entities;

#endregion

namespace School.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current ClassesProviderBase instance.
		///</summary>
		public virtual ClassesProviderBase ClassesProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current StudentsProviderBase instance.
		///</summary>
		public virtual StudentsProviderBase StudentsProvider{get {throw new NotImplementedException();}}
		
		
	}
}
