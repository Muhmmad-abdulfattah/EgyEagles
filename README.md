# EgyEagles Management System

نظام متكامل لإدارة الشركات والمركبات والمستخدمين باستخدام ASP.NET Core وMongoDB.  
يحتوي على Web API وواجهة MVC مبنية على 3-Tier Architecture.

---

## 📁 مكونات المشروع

### 1. EgyEagles.API
- Web API مبنية بـ ASP.NET Core
- توثيق باستخدام JWT
- تحكم بالأدوار (SuperAdmin / CompanyAdmin / RegularUser)
- يدعم العمليات الكاملة (CRUD) على:
  - الشركات (Companies)
  - المركبات (Vehicles)
  - المستخدمين (Users)

### 2. EgyEagles.MVC
- واجهة أمامية (Client) تتعامل مع الـ API
- تسجيل دخول باستخدام JWT وتخزين في Session
- عرض البيانات والتحكم فيها حسب الدور
- صفحات كاملة لإدارة:
  - تسجيل الدخول
  - الشركات
  - المركبات
  - المستخدمين

### 3. EgyEagles.BLL
- طبقة منطق الأعمال (Business Logic Layer)
- توليد JWT Tokens
- التحقق من المستخدمين وصلاحياتهم

### 4. EgyEagles.DAL
- طبقة التعامل مع قاعدة البيانات MongoDB
- Repositories عامة ومتخصصة
- MongoDbContext مُخصص للربط مع قاعدة البيانات

### 5. EgyEagles.Domain
- يحتوي على الكيانات الأساسية (Entities)
- تعريف الأدوار (Enums) والصلاحيات

### 6. EgyEagles.Shared
- يحتوي على DTOs المستخدمة بين الطبقات

---

## 🛠 المتطلبات

- .NET 8 أو أعلى
- MongoDB (محليًا على mongodb://localhost:27017)
- Visual Studio 2022 أو أي محرر .NET
- Swagger أو Postman لتجربة الـ API

---

## 🧪 بيانات أولية (Seed Data)

يتم توليدها تلقائيًا أول مرة عند تشغيل الـ API:

| النوع         | البريد                 | كلمة المرور |
|---------------|------------------------|-------------|
| Super Admin   | super@admin.com        | 123456      |
| Company Admin | admin@demo.com         | 123456      |
| User          | user@demo.com          | 123456      |
| Vehicles      | Toyota / Nissan        | الشركة: Demo Company |

---

## 🧭 الأدوار والصلاحيات

- *SuperAdmin*:
  - يشاهد كل الشركات والمستخدمين والمركبات
  - يقدر يضيف ويعدل ويحذف

- *CompanyAdmin*:
  - يرى شركته فقط
  - يضيف/يعدل المستخدمين والمركبات داخل شركته

- *User*:
  - يرى البيانات فقط بدون صلاحيات تعديل

---

## ▶ طريقة التشغيل

1. تأكد أن MongoDB يعمل على جهازك
2. شغّل المشروع من Visual Studio واختر EgyEagles.API كبداية
3. استخدم Swagger لتجربة الـ API
4. افتح EgyEagles.MVC لتجربة الواجهة
5. سجّل دخول باستخدام أحد الحسابات الجاهزة

---

## 📌 ملاحظات

- تم استخدام Session في MVC لتخزين الـ Token والدور
- تم تفعيل سياسة Authorize في كل Controller
- تم استخدام JWT وتوثيق Swagger بتفاصيل التوكن

---

## 🧠 بنية المشروع (3-Tier Architecture)

- *Presentation Layer*: API + MVC
- *Business Logic Layer*: EgyEagles.BLL
- *Data Access Layer*: EgyEagles.DAL
- *Domain & DTOs*: Entities و Shared
