namespace MoneyManger
{
    public static class Constants
    {
        public const string DB_NAME = "MoneyManger.db";

        #region Basic Label Message
        public const string OK_LABEL = "OK";
        public const string CANCEL_LABEL = "Cancel";
        public const string ERROR_LABEL = "Error";
        #endregion

        #region Person Dialog Message
        public const string PERSON_DIALOG_PLACE_HOLDER = "Name";

        public const string PERSON_ADD_DIALOG_TITLE = "Add New Entity";
        public const string PERSON_ADD_DIALOG_MESSAGE = "What is the name of the entity you want to manage money for?";
        
        public const string PERSON_EDIT_DIALOG_TITLE = "Edit Entity";
        public const string PERSON_EDIT_DIALOG_MESSAGE = "What is the name of the entity you want to update to?";

        public const string PERSON_DELETE_DIALOG_TITLE = "Delete Entity";
        public const string PERSON_DELETE_DIALOG_MESSAGE = "Are you sure you want to delete this entity?";

        public const string PERSON_ADD_SUCCESS = "Entity added successfully.";
        public const string PERSON_ADD_FAILED = "Entity added failed.";
        public const string PERSON_UPDATE_SUCCESS = "Entity updated successfully.";
        public const string PERSON_UPDATE_FAILED = "Entity updated failed.";
        public const string PERSON_DELETE_SUCCESS = "Entity deleted successfully.";
        public const string PERSON_DELETE_FAILED = "Entity deleted failed.";
        #endregion


        #region Transaction Dialog Message
        public const string TRANSACTION_ADD_DIALOG_TITLE = "Add New Transaction";
        public const string TRANSACTION_ADD_DIALOG_MESSAGE = "What is the name of the entity you want to manage money for?";

        public const string TRANSACTION_EDIT_DIALOG_TITLE = "Edit Transaction";
        public const string TRANSACTION_EDIT_DIALOG_MESSAGE = "What is the name of the entity you want to update to?";

        public const string TRANSACTION_DELETE_DIALOG_TITLE = "Delete Entity Name";
        public const string TRANSACTION_DELETE_DIALOG_MESSAGE = "Are you sure you want to delete this transaction?";

        public const string TRANSACTION_ADD_SUCCESS = "Transaction added successfully.";
        public const string TRANSACTION_ADD_FAILED = "Transaction was not added successfully.";
        public const string TRANSACTION_UPDATE_SUCCESS = "Transaction updated successfully.";
        public const string TRANSACTION_UPDATE_FAILED = "Transaction updated failed.";
        public const string TRANSACTION_DELETE_SUCCESS = "Transaction deleted successfully.";
        public const string TRANSACTION_DELETE_FAILED = "Transaction deleted failed.";
        #endregion

        #region Error and validation Message
        public const string PERSON_NAME_EMPTY = "Entity name can not be empty.";
        public const string PERSON_NULL = "Entity can not be null or empty.";
        public const string PERSON_NON_ZERO_VALIDATION_FAILED = "PersonId cannot be less then or equal to zero.";

        public const string TRANSACTION_NULL = "Transaction can not be null or empty.";
        public const string TRANSACTION_NON_ZERO_VALIDATION_FAILED = "TransactionId cannot be less then or equal to zero.";
        #endregion
    }
}
