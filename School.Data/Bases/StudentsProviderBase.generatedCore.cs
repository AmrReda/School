#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using School.Entities;
using School.Data;

#endregion

namespace School.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="StudentsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class StudentsProviderBaseCore : EntityProviderBase<School.Entities.Students, School.Entities.StudentsKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, School.Entities.StudentsKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Int32 _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Int32 _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		FK_Students_Classes1 Description: 
		/// </summary>
		/// <param name="_classId"></param>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		public TList<Students> GetByClassId(System.Int32 _classId)
		{
			int count = -1;
			return GetByClassId(_classId, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		FK_Students_Classes1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_classId"></param>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		/// <remarks></remarks>
		public TList<Students> GetByClassId(TransactionManager transactionManager, System.Int32 _classId)
		{
			int count = -1;
			return GetByClassId(transactionManager, _classId, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		FK_Students_Classes1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_classId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		public TList<Students> GetByClassId(TransactionManager transactionManager, System.Int32 _classId, int start, int pageLength)
		{
			int count = -1;
			return GetByClassId(transactionManager, _classId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		fkStudentsClasses1 Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_classId"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		public TList<Students> GetByClassId(System.Int32 _classId, int start, int pageLength)
		{
			int count =  -1;
			return GetByClassId(null, _classId, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		fkStudentsClasses1 Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_classId"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		public TList<Students> GetByClassId(System.Int32 _classId, int start, int pageLength,out int count)
		{
			return GetByClassId(null, _classId, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_Students_Classes1 key.
		///		FK_Students_Classes1 Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_classId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of School.Entities.Students objects.</returns>
		public abstract TList<Students> GetByClassId(TransactionManager transactionManager, System.Int32 _classId, int start, int pageLength, out int count);
		
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override School.Entities.Students Get(TransactionManager transactionManager, School.Entities.StudentsKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Students index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public School.Entities.Students GetById(System.Int32 _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Students index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public School.Entities.Students GetById(System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Students index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public School.Entities.Students GetById(TransactionManager transactionManager, System.Int32 _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Students index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public School.Entities.Students GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Students index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public School.Entities.Students GetById(System.Int32 _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Students index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="School.Entities.Students"/> class.</returns>
		public abstract School.Entities.Students GetById(TransactionManager transactionManager, System.Int32 _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Students&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Students&gt;"/></returns>
		public static TList<Students> Fill(IDataReader reader, TList<Students> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				School.Entities.Students c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Students")
					.Append("|").Append((System.Int32)reader[((int)StudentsColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Students>(
					key.ToString(), // EntityTrackingKey
					"Students",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new School.Entities.Students();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.Id = (System.Int32)reader[((int)StudentsColumn.Id - 1)];
					c.Name = (System.String)reader[((int)StudentsColumn.Name - 1)];
					c.Address = (System.String)reader[((int)StudentsColumn.Address - 1)];
					c.ClassId = (System.Int32)reader[((int)StudentsColumn.ClassId - 1)];
					c.Birthdate = (System.DateTime)reader[((int)StudentsColumn.Birthdate - 1)];
					c.Gender = (System.String)reader[((int)StudentsColumn.Gender - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="School.Entities.Students"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="School.Entities.Students"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, School.Entities.Students entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Int32)reader[((int)StudentsColumn.Id - 1)];
			entity.Name = (System.String)reader[((int)StudentsColumn.Name - 1)];
			entity.Address = (System.String)reader[((int)StudentsColumn.Address - 1)];
			entity.ClassId = (System.Int32)reader[((int)StudentsColumn.ClassId - 1)];
			entity.Birthdate = (System.DateTime)reader[((int)StudentsColumn.Birthdate - 1)];
			entity.Gender = (System.String)reader[((int)StudentsColumn.Gender - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="School.Entities.Students"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="School.Entities.Students"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, School.Entities.Students entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Int32)dataRow["Id"];
			entity.Name = (System.String)dataRow["Name"];
			entity.Address = (System.String)dataRow["Address"];
			entity.ClassId = (System.Int32)dataRow["ClassId"];
			entity.Birthdate = (System.DateTime)dataRow["Birthdate"];
			entity.Gender = (System.String)dataRow["Gender"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="School.Entities.Students"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">School.Entities.Students Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, School.Entities.Students entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region ClassIdSource	
			if (CanDeepLoad(entity, "Classes|ClassIdSource", deepLoadType, innerList) 
				&& entity.ClassIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.ClassId;
				Classes tmpEntity = EntityManager.LocateEntity<Classes>(EntityLocator.ConstructKeyFromPkItems(typeof(Classes), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.ClassIdSource = tmpEntity;
				else
					entity.ClassIdSource = DataRepository.ClassesProvider.GetById(transactionManager, entity.ClassId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'ClassIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.ClassIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.ClassesProvider.DeepLoad(transactionManager, entity.ClassIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion ClassIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the School.Entities.Students object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">School.Entities.Students instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">School.Entities.Students Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, School.Entities.Students entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region ClassIdSource
			if (CanDeepSave(entity, "Classes|ClassIdSource", deepSaveType, innerList) 
				&& entity.ClassIdSource != null)
			{
				DataRepository.ClassesProvider.Save(transactionManager, entity.ClassIdSource);
				entity.ClassId = entity.ClassIdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region StudentsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>School.Entities.Students</c>
	///</summary>
	public enum StudentsChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Classes</c> at ClassIdSource
		///</summary>
		[ChildEntityType(typeof(Classes))]
		Classes,
	}
	
	#endregion StudentsChildEntityTypes
	
	#region StudentsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;StudentsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StudentsFilterBuilder : SqlFilterBuilder<StudentsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StudentsFilterBuilder class.
		/// </summary>
		public StudentsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StudentsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StudentsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StudentsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StudentsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StudentsFilterBuilder
	
	#region StudentsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;StudentsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class StudentsParameterBuilder : ParameterizedSqlFilterBuilder<StudentsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StudentsParameterBuilder class.
		/// </summary>
		public StudentsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the StudentsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public StudentsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the StudentsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public StudentsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion StudentsParameterBuilder
	
	#region StudentsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;StudentsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Students"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class StudentsSortBuilder : SqlSortBuilder<StudentsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the StudentsSqlSortBuilder class.
		/// </summary>
		public StudentsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion StudentsSortBuilder
	
} // end namespace
