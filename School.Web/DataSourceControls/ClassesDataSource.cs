#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using School.Entities;
using School.Data;
using School.Data.Bases;
#endregion

namespace School.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.ClassesProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(ClassesDataSourceDesigner))]
	public class ClassesDataSource : ProviderDataSource<Classes, ClassesKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClassesDataSource class.
		/// </summary>
		public ClassesDataSource() : base(DataRepository.ClassesProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the ClassesDataSourceView used by the ClassesDataSource.
		/// </summary>
		protected ClassesDataSourceView ClassesView
		{
			get { return ( View as ClassesDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the ClassesDataSource control invokes to retrieve data.
		/// </summary>
		public ClassesSelectMethod SelectMethod
		{
			get
			{
				ClassesSelectMethod selectMethod = ClassesSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (ClassesSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the ClassesDataSourceView class that is to be
		/// used by the ClassesDataSource.
		/// </summary>
		/// <returns>An instance of the ClassesDataSourceView class.</returns>
		protected override BaseDataSourceView<Classes, ClassesKey> GetNewDataSourceView()
		{
			return new ClassesDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the ClassesDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class ClassesDataSourceView : ProviderDataSourceView<Classes, ClassesKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ClassesDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the ClassesDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public ClassesDataSourceView(ClassesDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal ClassesDataSource ClassesOwner
		{
			get { return Owner as ClassesDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal ClassesSelectMethod SelectMethod
		{
			get { return ClassesOwner.SelectMethod; }
			set { ClassesOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal ClassesProviderBase ClassesProvider
		{
			get { return Provider as ClassesProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Classes> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Classes> results = null;
			Classes item;
			count = 0;
			
			System.Int32 _id;

			switch ( SelectMethod )
			{
				case ClassesSelectMethod.Get:
					ClassesKey entityKey  = new ClassesKey();
					entityKey.Load(values);
					item = ClassesProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Classes>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case ClassesSelectMethod.GetAll:
                    results = ClassesProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case ClassesSelectMethod.GetPaged:
					results = ClassesProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case ClassesSelectMethod.Find:
					if ( FilterParameters != null )
						results = ClassesProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = ClassesProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case ClassesSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = ClassesProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Classes>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				// M:M
				// Custom
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == ClassesSelectMethod.Get || SelectMethod == ClassesSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Classes entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					ClassesProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<Classes> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			ClassesProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region ClassesDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the ClassesDataSource class.
	/// </summary>
	public class ClassesDataSourceDesigner : ProviderDataSourceDesigner<Classes, ClassesKey>
	{
		/// <summary>
		/// Initializes a new instance of the ClassesDataSourceDesigner class.
		/// </summary>
		public ClassesDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ClassesSelectMethod SelectMethod
		{
			get { return ((ClassesDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new ClassesDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region ClassesDataSourceActionList

	/// <summary>
	/// Supports the ClassesDataSourceDesigner class.
	/// </summary>
	internal class ClassesDataSourceActionList : DesignerActionList
	{
		private ClassesDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the ClassesDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public ClassesDataSourceActionList(ClassesDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public ClassesSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion ClassesDataSourceActionList
	
	#endregion ClassesDataSourceDesigner
	
	#region ClassesSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the ClassesDataSource.SelectMethod property.
	/// </summary>
	public enum ClassesSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion ClassesSelectMethod

	#region ClassesFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Classes"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClassesFilter : SqlFilter<ClassesColumn>
	{
	}
	
	#endregion ClassesFilter

	#region ClassesExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Classes"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClassesExpressionBuilder : SqlExpressionBuilder<ClassesColumn>
	{
	}
	
	#endregion ClassesExpressionBuilder	

	#region ClassesProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;ClassesChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Classes"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ClassesProperty : ChildEntityProperty<ClassesChildEntityTypes>
	{
	}
	
	#endregion ClassesProperty
}

