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
	/// Represents the DataRepository.StudentsProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(StudentsDataSourceDesigner))]
	public class StudentsDataSource : ProviderDataSource<Students, StudentsKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StudentsDataSource class.
		/// </summary>
		public StudentsDataSource() : base(DataRepository.StudentsProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the StudentsDataSourceView used by the StudentsDataSource.
		/// </summary>
		protected StudentsDataSourceView StudentsView
		{
			get { return ( View as StudentsDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the StudentsDataSource control invokes to retrieve data.
		/// </summary>
		public StudentsSelectMethod SelectMethod
		{
			get
			{
				StudentsSelectMethod selectMethod = StudentsSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (StudentsSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the StudentsDataSourceView class that is to be
		/// used by the StudentsDataSource.
		/// </summary>
		/// <returns>An instance of the StudentsDataSourceView class.</returns>
		protected override BaseDataSourceView<Students, StudentsKey> GetNewDataSourceView()
		{
			return new StudentsDataSourceView(this, DefaultViewName);
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
	/// Supports the StudentsDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class StudentsDataSourceView : ProviderDataSourceView<Students, StudentsKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StudentsDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the StudentsDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public StudentsDataSourceView(StudentsDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal StudentsDataSource StudentsOwner
		{
			get { return Owner as StudentsDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal StudentsSelectMethod SelectMethod
		{
			get { return StudentsOwner.SelectMethod; }
			set { StudentsOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal StudentsProviderBase StudentsProvider
		{
			get { return Provider as StudentsProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Students> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Students> results = null;
			Students item;
			count = 0;
			
			System.Int32 _id;
			System.Int32 _classId;

			switch ( SelectMethod )
			{
				case StudentsSelectMethod.Get:
					StudentsKey entityKey  = new StudentsKey();
					entityKey.Load(values);
					item = StudentsProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Students>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case StudentsSelectMethod.GetAll:
                    results = StudentsProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case StudentsSelectMethod.GetPaged:
					results = StudentsProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case StudentsSelectMethod.Find:
					if ( FilterParameters != null )
						results = StudentsProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = StudentsProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case StudentsSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Id"], typeof(System.Int32)) : (int)0;
					item = StudentsProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Students>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case StudentsSelectMethod.GetByClassId:
					_classId = ( values["ClassId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["ClassId"], typeof(System.Int32)) : (int)0;
					results = StudentsProvider.GetByClassId(GetTransactionManager(), _classId, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == StudentsSelectMethod.Get || SelectMethod == StudentsSelectMethod.GetById )
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
				Students entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					StudentsProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Students> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			StudentsProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region StudentsDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the StudentsDataSource class.
	/// </summary>
	public class StudentsDataSourceDesigner : ProviderDataSourceDesigner<Students, StudentsKey>
	{
		/// <summary>
		/// Initializes a new instance of the StudentsDataSourceDesigner class.
		/// </summary>
		public StudentsDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StudentsSelectMethod SelectMethod
		{
			get { return ((StudentsDataSource) DataSource).SelectMethod; }
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
				actions.Add(new StudentsDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region StudentsDataSourceActionList

	/// <summary>
	/// Supports the StudentsDataSourceDesigner class.
	/// </summary>
	internal class StudentsDataSourceActionList : DesignerActionList
	{
		private StudentsDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the StudentsDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public StudentsDataSourceActionList(StudentsDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public StudentsSelectMethod SelectMethod
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

	#endregion StudentsDataSourceActionList
	
	#endregion StudentsDataSourceDesigner
	
	#region StudentsSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the StudentsDataSource.SelectMethod property.
	/// </summary>
	public enum StudentsSelectMethod
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
		GetById,
		/// <summary>
		/// Represents the GetByClassId method.
		/// </summary>
		GetByClassId
	}
	
	#endregion StudentsSelectMethod

	#region StudentsFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StudentsFilter : SqlFilter<StudentsColumn>
	{
	}
	
	#endregion StudentsFilter

	#region StudentsExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StudentsExpressionBuilder : SqlExpressionBuilder<StudentsColumn>
	{
	}
	
	#endregion StudentsExpressionBuilder	

	#region StudentsProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;StudentsChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StudentsProperty : ChildEntityProperty<StudentsChildEntityTypes>
	{
	}
	
	#endregion StudentsProperty
}

