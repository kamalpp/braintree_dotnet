#pragma warning disable 1591

using System;

namespace Braintree
{
    public class WebhookKind : Enumeration
    {
        public static readonly WebhookKind PARTNER_MERCHANT_CONNECTED = new WebhookKind("partner_merchant_connected");
        public static readonly WebhookKind PARTNER_MERCHANT_DISCONNECTED = new WebhookKind("partner_merchant_disconnected");
        public static readonly WebhookKind PARTNER_MERCHANT_DECLINED = new WebhookKind("partner_merchant_declined");
        public static readonly WebhookKind SUBSCRIPTION_CANCELED = new WebhookKind("subscription_canceled");
        public static readonly WebhookKind SUBSCRIPTION_CHARGED_SUCCESSFULLY = new WebhookKind("subscription_charged_successfully");
        public static readonly WebhookKind SUBSCRIPTION_CHARGED_UNSUCCESSFULLY = new WebhookKind("subscription_charged_unsuccessfully");
        public static readonly WebhookKind SUBSCRIPTION_EXPIRED = new WebhookKind("subscription_expired");
        public static readonly WebhookKind SUBSCRIPTION_TRIAL_ENDED = new WebhookKind("subscription_trial_ended");
        public static readonly WebhookKind SUBSCRIPTION_WENT_ACTIVE = new WebhookKind("subscription_went_active");
        public static readonly WebhookKind SUBSCRIPTION_WENT_PAST_DUE = new WebhookKind("subscription_went_past_due");
        public static readonly WebhookKind SUB_MERCHANT_ACCOUNT_APPROVED = new WebhookKind("sub_merchant_account_approved");
        public static readonly WebhookKind SUB_MERCHANT_ACCOUNT_DECLINED = new WebhookKind("sub_merchant_account_declined");
        public static readonly WebhookKind UNRECOGNIZED = new WebhookKind("unrecognized");
        public static readonly WebhookKind TRANSACTION_DISBURSED = new WebhookKind("transaction_disbursed");
        public static readonly WebhookKind TRANSFER_EXCEPTION = new WebhookKind("transfer_exception");

        public static readonly WebhookKind[] ALL = {
            PARTNER_MERCHANT_CONNECTED,
            PARTNER_MERCHANT_DISCONNECTED,
            PARTNER_MERCHANT_DECLINED,
            SUBSCRIPTION_CANCELED,
            SUBSCRIPTION_CHARGED_SUCCESSFULLY,
            SUBSCRIPTION_CHARGED_UNSUCCESSFULLY,
            SUBSCRIPTION_EXPIRED,
            SUBSCRIPTION_TRIAL_ENDED,
            SUBSCRIPTION_WENT_ACTIVE,
            SUBSCRIPTION_WENT_PAST_DUE,
            SUB_MERCHANT_ACCOUNT_APPROVED,
            SUB_MERCHANT_ACCOUNT_DECLINED,
            TRANSACTION_DISBURSED,
            TRANSFER_EXCEPTION
        };

        protected WebhookKind(String name) : base(name) {}
    }

    public class WebhookNotification
    {
        public WebhookKind Kind { get; protected set; }
        public Subscription Subscription { get; protected set; }
        public MerchantAccount MerchantAccount { get; protected set; }
        public ValidationErrors Errors { get; protected set; }
        public String Message { get; protected set; }
        public DateTime? Timestamp { get; protected set; }
        public Transaction Transaction { get; protected set; }
        public Transfer Transfer { get; protected set; }
        public PartnerMerchant PartnerMerchant { get; protected set; }

        public WebhookNotification(NodeWrapper node, BraintreeService service)
        {
            Timestamp = node.GetDateTime("timestamp");
            Kind = (WebhookKind)CollectionUtil.Find(WebhookKind.ALL, node.GetString("kind"), WebhookKind.UNRECOGNIZED);

            NodeWrapper WrapperNode = node.GetNode("subject");

            if (WrapperNode.GetNode("api-error-response") != null) {
                WrapperNode = WrapperNode.GetNode("api-error-response");
            }

            if (WrapperNode.GetNode("subscription") != null) {
                Subscription = new Subscription(WrapperNode.GetNode("subscription"), service);
            }

            if (WrapperNode.GetNode("merchant-account") != null) {
                MerchantAccount = new MerchantAccount(WrapperNode.GetNode("merchant-account"));
            }

            if (WrapperNode.GetNode("transaction") != null) {
                Transaction = new Transaction(WrapperNode.GetNode("transaction"), service);
            }

            if (WrapperNode.GetNode("transfer") != null) {
                Transfer = new Transfer(WrapperNode.GetNode("transfer"), service);
            }

            if (WrapperNode.GetNode("partner-merchant") != null) {
                PartnerMerchant = new PartnerMerchant(WrapperNode.GetNode("partner-merchant"));
            }

            if (WrapperNode.GetNode("errors") != null) {
                Errors = new ValidationErrors(WrapperNode.GetNode("errors"));
            }

            if (WrapperNode.GetNode("message") != null) {
                Message = WrapperNode.GetString("message");
            }
        }
    }
}
