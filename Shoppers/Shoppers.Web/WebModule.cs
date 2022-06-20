using Autofac;
using Shoppers.Web.Areas.Admin.Models;
using Shoppers.Web.Areas.Store.Models;
using Shoppers.Utility;
using Shoppers.Web.Areas.SuperAdmin.Models;
using Shoppers.Web.Models;

namespace Shoppers.Web
{
    public class WebModule : Module
    {    
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductCreateModel>().AsSelf();
            builder.RegisterType<ProductEditModel>().AsSelf();
            builder.RegisterType<ProductListModel>().AsSelf();
            builder.RegisterType<Discount>().As<IDiscount>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StoreCreateModel>().AsSelf();
            builder.RegisterType<StoreEditModel>().AsSelf();
            builder.RegisterType<StoreListModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<CustomerRegisterModel>().AsSelf();
            builder.RegisterType<RegistrationConfirmationModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<FrontProductListModel>().AsSelf();
            builder.RegisterType<CategoryListModel>().AsSelf();
            builder.RegisterType<CategoryEditModel>().AsSelf(); 
            builder.RegisterType<ProductDetailsModel>().AsSelf(); 
            builder.RegisterType<OrderListModel>().AsSelf();
            builder.RegisterType<OrderEditModel>().AsSelf();

            builder.RegisterType<InventoryListModel>().AsSelf();
            builder.RegisterType<InventoryUpdateModel>().AsSelf();

            builder.RegisterType<SuperAdminLoginModel>().AsSelf();
            builder.RegisterType<ForgotPasswordModel>().AsSelf();
            builder.RegisterType<ResetPasswordModel>().AsSelf();
            builder.RegisterType<EmailSender>().As<IEmailSender>();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<RegistrationConfirmationModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();

            base.Load(builder);
        }
    }
}
