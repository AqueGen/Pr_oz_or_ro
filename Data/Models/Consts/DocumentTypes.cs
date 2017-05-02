namespace Kapitalist.Data.Models.Consts
{
	/// <summary>
	/// Можливі значення DocumentTypes
	/// </summary>
	public static class DocumentTypes
	{
		/// <summary>
		/// Можливі значення DocumentTypes для Tender
		/// </summary>
		public static class Tender
		{
			/// <summary>
			/// Повідомлення про закупівлю
			/// Офіційне повідомлення, що містить деталі закупівлі.
			/// Це може бути посилання на документ, веб-сторінку, чи на офіційний бюлетень, де розміщено повідомлення.
			/// </summary>
			public const string NOTICE = "notice";

			/// <summary>
			/// Документи закупівлі
			/// Інформація для потенційних постачальників, що описує цілі договору
			/// (наприклад, товари та послуги, які будуть закуплені) і процес торгів.
			/// </summary>
			public const string BIDDING_DOCUMENTS = "biddingDocuments";

			/// <summary>
			/// Технічні специфікації
			/// Детальна технічна інформація про товари або послуги, що повинні бути надані.
			/// </summary>
			public const string TECHNICAL_SPECIFICATIONS = "technicalSpecifications";

			/// <summary>
			/// Критерії оцінки
			/// Інформація про те, як будуть оцінюватись пропозиції.
			/// </summary>
			public const string EVALUATION_CRITERIA = "evaluationCriteria";

			/// <summary>
			/// Пояснення до питань заданих учасниками
			/// Включає відповіді на питання, підняті на передтендерних конференціях.
			/// </summary>
			public const string CLARIFICATIONS = "clarifications";

			/// <summary>
			/// Критерії прийнятності
			/// Докладні документи про критерії відбору.
			/// </summary>
			public const string ELIGIBILITY_CRITERIA = "eligibilityCriteria";

			/// <summary>
			/// Фірми у короткому списку
			/// </summary>
			public const string SHORTLISTED_FIRMS = "shortlistedFirms";

			/// <summary>
			/// Положення для управління ризиками та зобов’язаннями
			/// </summary>
			public const string RISK_PROVISIONS = "riskProvisions";

			/// <summary>
			/// Кошторис
			/// </summary>
			public const string BILL_OF_QUANTITY = "billOfQuantity";

			/// <summary>
			/// Інформація про учасників
			/// Інформація про учасників, їхні документи для перевірки 
			/// та будь-які процесуальні пільги, на які вони можуть претендувати.
			/// </summary>
			public const string BIDDERS = "bidders";

			/// <summary>
			/// Виявлені конфлікти інтересів
			/// </summary>
			public const string CONFLICT_OF_INTEREST = "conflictOfInterest";

			/// <summary>
			/// Відмова у допуску до закупівлі
			/// </summary>
			public const string DEBARMENTS = "debarments";

			/// <summary>
			/// Проект договору
			/// </summary>
			public const string CONTRACT_PROFORMA = "contractProforma";
		}

		/// <summary>
		/// Можливі значення DocumentTypes для Award
		/// </summary>
		public static class Award
		{
			/// <summary>
			/// Повідомлення про рішення
			/// Офіційне повідомлення, що містить деталі рішення про визначення переможця.
			/// Це може бути посилання на документ, веб-сторінку, чи на офіційний бюлетень, де розміщено повідомлення.
			/// </summary>
			public const string NOTICE = "notice";

			/// <summary>
			/// Звіт про оцінку
			/// Звіт про оцінку пропозицій і застосування критеріїв оцінки, 
			/// у тому числі, обґрунтування рішення про визначення переможця.
			/// </summary>
			public const string EVALUATION_REPORTS = "evaluationReports";

			/// <summary>
			/// Пропозиція, що перемогла
			/// </summary>
			public const string WINNING_BID = "winningBid";

			/// <summary>
			/// Скарги та рішення
			/// </summary>
			public const string COMPLAINTS = "complaints";
		}

		/// <summary>
		/// Можливі значення DocumentTypes для Contract
		/// </summary>
		public static class Contract
		{
			/// <summary>
			/// Повідомлення про договір
			/// Офіційне повідомлення, що містить деталі підписання доовору та початку його реалізації.
			/// Це може бути посилання на документ, веб-сторінку, чи на офіційний бюлетень, де розміщено повідомлення.
			/// </summary>
			public const string NOTICE = "notice";

			/// <summary>
			/// Підписаний договір
			/// </summary>
			public const string CONTRACT_SIGNED = "contractSigned";

			/// <summary>
			/// Заходи для припинення договору
			/// </summary>
			public const string CONTRACT_ARRANGEMENTS = "contractArrangements";

			/// <summary>
			/// Розклад та етапи
			/// </summary>
			public const string CONTRACT_SCHEDULE = "contractSchedule";

			/// <summary>
			/// Додатки до договору
			/// </summary>
			public const string CONTRACT_ANNEXE = "contractAnnexe";

			/// <summary>
			/// Забезпечення тендерних пропозицій
			/// </summary>
			public const string CONTRACT_GUARANTEES = "contractGuarantees";

			/// <summary>
			/// Субпідряд
			/// </summary>
			public const string SUB_CONTRACT = "subContract";
		}

		/// <summary>
		/// Можливі значення DocumentTypes для Bid
		/// </summary>
		public static class Bid
		{
			/// <summary>
			/// Цінова пропозиція
			/// </summary>
			public const string COMMERCIAL_PROPOSAL = "commercialProposal";

			/// <summary>
			/// Документи, що підтверджують кваліфікацію
			/// </summary>
			public const string QUALIFICATION_DOCUMENTS = "qualificationDocuments";

			/// <summary>
			/// Документи, що підтверджують відповідність (в тому числі, відповідність вимогам ст. 17)
			/// </summary>
			public const string ELIGIBILITY_DOCUMENTS = "eligibilityDocuments";
		}
	}
}