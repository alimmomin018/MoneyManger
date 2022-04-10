namespace MoneyManger
{
    public static class Constants
    {
        public const string DB_NAME = "MoneyManger.db";

        #region Basic Label Names
        public const string OK_LABEL = "OK";
        public const string CANCEL_LABEL = "Cancel";
        public const string ERROR_LABEL = "Error";
        #endregion

        #region Person Dialogs
        public const string PERSON_DIALOG_PLACE_HOLDER = "Name";
        public const string PERSON_ADD_EMPTY = "Entity name can not be empty.";

        public const string PERSON_ADD_DIALOG_TITLE = "Add Entity Name";
        public const string PERSON_ADD_DIALOG_MESSAGE = "What is the name of the entity you want to manage money for?";
        
        public const string PERSON_EDIT_DIALOG_TITLE = "Edit Entity Name";
        public const string PERSON_EDIT_DIALOG_MESSAGE = "What is the name of the entity you want to update to?";

        public const string PERSON_DELETE_DIALOG_TITLE = "Delete Entity Name";
        public const string PERSON_DELETE_DIALOG_MESSAGE = "Are you sure you want to delete?";

        public const string PERSON_ADD_SUCCESS = "Entity added successfully.";
        public const string PERSON_UPDATED_SUCCESS = "Entity updated successfully.";
        public const string PERSON_DELETE_SUCCESS = "Entity deleted successfully.";
        #endregion

        #region Success
        #endregion

        #region Errors and Success
        public const string PERSON_ADD_FAILED = "Entity was not added successfully.";
        public const string PERSON_NULL = "Entity can not be null or empty.";
        public const string PERSON_NON_ZERO_VALIDATION_FAILED = "PersonId cannot be less then or equal to zero.";

        public const string TRANSACTION_ADD_SUCCESS = "Entity added successfully.";
        public const string TRANSACTION_ADD_FAILED = "Entity was not added successfully.";
        public const string TRANSACTION_NON_ZERO_VALIDATION_FAILED = "TransactionId cannot be less then or equal to zero.";
        public const string TRANSACTION_NULL = "Transaction can not be null or empty.";
        #endregion
    }
}
