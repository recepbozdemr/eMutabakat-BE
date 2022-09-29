﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constance
{
    public class Messages
    {
        public static string AddEdCompany="Şirket kaydı başarıyla tamamlandı";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string UserRegistered = "Kayıt Başarılı";
        public static string AccessTokenCreated = "Token Oluşturuldu";
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserNotFoundForCompany = "Şirket için kullanıcı bulunamadı";
        public static string UserNotFoundForCompanyAndUser = "Şirket için kullanıcı bulunamadı";
        public static string UserNotFoundForCompanyAndUserAndRole = "Şirket için kullanıcı bulunamadı";
        public static string UserNotFoundForCompanyAndUserAndRoleAndOperation = "Şirket için kullanıcı bulunamadı";
        public static string UserNotFoundForCompanyAndUserAndRoleAndOperationAndOperationClaim = "Şirket için kullanıcı bulunamadı";
        public static string CompanyAlreadyExist = "Bu şirket daha önce kaydedilmiş"; 
        public static string MailParemeterUpdated = "Mail parametreleri başarılı bir şekilde güncellendi";
        public static string MailParemeterNotFound = "Mail parametreleri bulunamadı";
        public static string MailSuccess = "Mail başarıyla gönderildi";
        public static string MailSuccessFull = "Onay Mail başarılı bir şekilde gönderildi";
        public static string UserMailConfirmeSuccess = "Mail başarıyla onaylandı";
        public static string MailError = "Mail gönderilirken bir hata oluştu";
        public static string MailTemplateAdded = "Mail şablonu başarıyla eklendi";
        public static string MailTemplateDeleted = "Mail şablonu başarıyla silindi";
        public static string MailTemplateUpdated = "Mail şablonu başarıyla güncellendi";
        public static string MailTemplateNotFound = "Mail şablonu bulunamadı";
        public static string MailTemplateListed = "Mail şablonları başarıyla listelendi";
        public static string MailTemplateListedForCompany = "Mail şablonları başarıyla listelendi"; 
        public static string MailAlready = "Mail onaylı tekrar gönderim yapılmadı"; 
        public static string MailConfirmTimeHasNotExpired = "Mail onayını 5 dakikada bir gönderebilirsiniz.";

    }
}
