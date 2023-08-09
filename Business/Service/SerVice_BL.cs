using System;
using System.Collections.Generic;
using VeiwModels;
using DataLayer.Contact;
using System.Globalization;
using System.Reflection.Emit;

namespace Business.Service
{
    public class SerVice_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool Exist(string customerNmae, string startDate, decimal comision)
        {
            return unit.Service.Exist(customerNmae, startDate, comision);
        }
        
        public bool InsertService(SerVice service)
        {
            if (service.PayType == "نقدی")
            {
                Transaction transaction = new Transaction();
                transaction.AccountSideId = service.CustomerAccountId;
                transaction.AccountSideName = service.CostomerName;
                transaction.Recived = service.Comision;
                transaction.Date = service.StartDate;
                transaction.Description = $"خدمات {service.DescriptionService}";
                transaction.Year = service.StartYear;
                transaction.Month = service.StartMonth;
                transaction.Day = service.StartDay;
                if (unit.TransactionGeneric.Insert(transaction))
                {
                    if (unit.ServiceGeneric.Insert(service))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else
            {
                if (unit.Account.AddToAccountDebt(service.CustomerAccountId, service.Comision))
                {
                    if (unit.ServiceGeneric.Insert(service))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        public bool DeleteService(SerVice service)
        {
            if (service.PayType == "نقدی")
            {
                //delete transction
                if (unit.TransactionGeneric.Delete(service.TransactionId))
                {
                    if (unit.ServiceGeneric.Delete(service))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (service.PayType == "نسیه")
            {
                //deduct from debt customer account
                if (unit.Account.DeductFromAccountDebt( service.CustomerAccountId, service.Comision))
                {
                    if (unit.ServiceGeneric.Delete(service))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateService(int oldServiceId, SerVice service)
        {
            var oldService = unit.ServiceGeneric.GetById(oldServiceId);
            service.ID = oldService.ID;
            if (oldService.PayType == "نقدی" && service.PayType == "نقدی")
            {
                //update transaction
                Transaction transaction = new Transaction();
                #region Get Data
                transaction = unit.TransactionGeneric.GetById(oldService.TransactionId);
                transaction.AccountSideId = service.CustomerAccountId;
                transaction.AccountSideName = service.CostomerName;
                transaction.Recived = service.Comision;
                transaction.Date = service.StartDate;
                transaction.Year = service.StartYear;
                transaction.Month = service.StartMonth;
                transaction.Day = service.StartDay;
                transaction.Description = service.DescriptionService;
                #endregion
                if (unit.TransactionGeneric.Update(transaction, transaction.ID))
                {
                    service.TransactionId = transaction.ID;
                    if (unit.ServiceGeneric.Update(service, service.ID))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (oldService.PayType == "نقدی" && service.PayType == "نسیه")
            {
                //remove transaction
                if (unit.TransactionGeneric.Delete(oldService.TransactionId))
                {
                    //add new debt to buyer account
                    if (unit.Account.AddToAccountDebt(service.CustomerAccountId, service.Comision))
                    {
                        if (unit.ServiceGeneric.Update(service, service.ID))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;

            }
            else if (oldService.PayType == "نسیه" && service.PayType == "نسیه")
            {
                //deduct old debt to buyer account
                if (unit.Account.DeductFromAccountDebt(service.CustomerAccountId, service.Comision))
                {
                    //add new debt to buyer account
                    if (unit.Account.AddToAccountDebt(service.CustomerAccountId, service.Comision))
                    {
                        if (unit.ServiceGeneric.Update(service, service.ID))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            else if (oldService.PayType == "نسیه" && service.PayType == "نقدی")
            {
                //deduct old debt from buyer account
                if (unit.Account.DeductFromAccountDebt(oldService.CustomerAccountId, oldService.Comision))
                {
                    //insert transaction
                    Transaction transaction = new Transaction();
                    #region Get Data
                    transaction = unit.TransactionGeneric.GetById(oldService.TransactionId);
                    transaction.AccountSideId = service.CustomerAccountId;
                    transaction.AccountSideName = service.CostomerName;
                    transaction.Recived = service.Comision;
                    transaction.Date = service.StartDate;
                    transaction.Year = service.StartYear;
                    transaction.Month = service.StartMonth;
                    transaction.Day = service.StartDay;
                    transaction.Description = service.DescriptionService;
                    #endregion
                    if (unit.TransactionGeneric.Insert(transaction))
                    {
                        service.TransactionId = transaction.ID;
                        if (unit.ServiceGeneric.Update(service, service.ID))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteService(int serviceId)
        {
            var service = unit.ServiceGeneric.GetById(serviceId);
            return DeleteService(service);
        }

        public SerVice GetServiceById(int serviceId)
        {
            return unit.ServiceGeneric.GetById(serviceId);
        }

        public List<SerVice> GetServicesByDescription(List<SerVice> filterServiceByDate, string description)
        {
            if (filterServiceByDate != null)
            {
                return unit.Service.GetSerVicesByDescription(filterServiceByDate, description);
            }
            else
            {
                return unit.Service.GetSerVicesByDescription(description);
            }
        }

        public List<SerVice> GetSerVicesByCustomerName(List<SerVice> filterServiceByDate, string customerName)
        {
            if (filterServiceByDate != null)
            {
                return unit.Service.GetSerVicesByCostomerName(filterServiceByDate, customerName);
            }
            else
            {
                return unit.Service.GetSerVicesByCostomerName(customerName);
            }
        }

        public List<SerVice> GetSerVicesByComision(List<SerVice> filterServiceByDate, decimal fromAmount, decimal toAmount) 
        {
            if (filterServiceByDate != null)
            {
                return unit.Service.GetSerVicesByComision(filterServiceByDate, fromAmount, toAmount);
            }
            else
            {
                return unit.Service.GetSerVicesByComision(fromAmount, toAmount);
            }
        }

        public List<SerVice> GetServicesByEndDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int currentYear = persianCalendar.GetYear(DateTime.Now);
            int currentMonth = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByEndDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByEndDay(currentYear, currentMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByEndMonth(currentYear, fromMonth, upToMonth);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByEndMonth_Day(currentYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByEndYear(fromYear, upToYear);
            }
            else if (fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByEndYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else
            {
                return null;
            }
        }
        
        public List<SerVice> GetServicesByStartDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int currentYear = persianCalendar.GetYear(DateTime.Now);
            int currentMonth = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByStartDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByStartDay(currentYear, currentMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByStartMonth(currentYear, fromMonth, upToMonth);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByStartMonth_Day(currentYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByStartYear(fromYear, upToYear);
            }
            else if (fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Service.GetServicesByStartYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Service.GetServicesByStartYear(fromYear, upToYear);
            }
            else
            {
                return null;
            }
        }

        public List<SerVice> GetSerVicesByNameOrDescription(string input)
        {
            return unit.Service.GetSerVicesByNameOrDescription(input);
        }
    }
}
