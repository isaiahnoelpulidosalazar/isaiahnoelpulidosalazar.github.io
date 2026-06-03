from easy_sql import EasySQL

import os.path
if not os.path.isfile("AUTOLANDIA.db"):
    a = EasySQL()

    AUTOLANDIA_ServiceList_columns = [
        {"ServiceId": "varchar"},
        {"ServiceName": "varchar"},
        {"ServicePriceSizeS": "float"},
        {"ServicePriceSizeM": "float"},
        {"ServicePriceSizeL": "float"},
        {"ServicePriceSizeXL": "float"},
        {"ServicePriceSizeXXL": "float"},
        {"ServiceStatus": "varchar"}
    ]
    AUTOLANDIA_PackageList_columns = [
        {"PackageId": "varchar"},
        {"PackageName": "varchar"},
        {"PackageDetails": "varchar"},
        {"PackagePriceSizeS": "float"},
        {"PackagePriceSizeM": "float"},
        {"PackagePriceSizeL": "float"},
        {"PackagePriceSizeXL": "float"},
        {"PackagePriceSizeXXL": "float"},
        {"PackageStatus": "varchar"}
    ]
    AUTOLANDIA_EmployeeList_columns = [
        {"EmployeeId": "varchar"},
        {"LastName": "varchar"},
        {"FirstName": "varchar"},
        {"MiddleName": "varchar"},
        {"EmployeeDocuments": "varchar"},
        {"MobileNumber": "varchar"},
        {"EmployeeAddress": "varchar"},
    ]
    AUTOLANDIA_EmployeeTimeList_columns = [
        {"EmployeeId": "varchar"},
        {"TimeIn": "varchar"},
        {"TimeOut": "varchar"},
        {"DateCreated": "varchar"}
    ]
    AUTOLANDIA_VehicleList_columns = [
        {"VehicleId": "varchar"},
        {"VehicleBrand": "varchar"},
        {"VehicleModel": "varchar"},
        {"VehicleSize": "varchar"},
        {"PlateNumber": "varchar"},
        {"CustomerId": "varchar"}
    ]
    AUTOLANDIA_CustomerList_columns = [
        {"CustomerId": "varchar"},
        {"LastName": "varchar"},
        {"FirstName": "varchar"},
        {"MiddleName": "varchar"},
        {"PlateNumbers": "varchar"},
        {"MobileNumber": "varchar"},
        {"CustomerAddress": "varchar"}
    ]
    AUTOLANDIA_ActivityList_columns = [
        {"ActivityMessage": "varchar"}
    ]
    AUTOLANDIA_TransactionList_columns = [
        {"TransactionId": "varchar"},
        {"EmployeeIdList": "varchar"},
        {"ServiceIdList": "varchar"},
        {"PackageId": "varchar"},
        {"Extras": "varchar"},
        {"VehicleId": "varchar"},
        {"TransactionStatus": "varchar"},
        {"DateUpdated": "varchar"},
        {"DateCreated": "varchar"}
    ]
    AUTOLANDIA_BillingList_columns = [
        {"BillingId": "varchar"},
        {"TransactionBalance": "float"},
        {"TransactionDiscount": "float"},
        {"BalancePaid": "float"},
        {"BillingStatus": "varchar"},
        {"DateUpdated": "varchar"},
        {"DateCreated": "varchar"},
    ]
    AUTOLANDIA_AdminList_columns = [
        {"Username": "varchar"},
        {"Password": "varchar"}
    ]
    AUTOLANDIA_UserList_columns = [
        {"Username": "varchar"},
        {"Password": "varchar"}
    ]

    a.createTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", AUTOLANDIA_ServiceList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_PackageList", AUTOLANDIA_PackageList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", AUTOLANDIA_EmployeeList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_EmployeeTimeList", AUTOLANDIA_EmployeeTimeList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_VehicleList", AUTOLANDIA_VehicleList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_CustomerList", AUTOLANDIA_CustomerList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_ActivityList", AUTOLANDIA_ActivityList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_TransactionList", AUTOLANDIA_TransactionList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_BillingList", AUTOLANDIA_BillingList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_AdminList", AUTOLANDIA_AdminList_columns)
    a.createTable("AUTOLANDIA", "AUTOLANDIA_UserList", AUTOLANDIA_UserList_columns)

    admin_value_0 = [
        {"Username": "admin"},
        {"Password": "admin"}
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_AdminList", admin_value_0)
    user_value_0 = [
        {"Username": "cashier"},
        {"Password": "cashier"}
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_UserList", user_value_0)

    employee_value_0 = [
        {"EmployeeId": "1"},
        {"FirstName": "DOMINIC"},
        {"LastName": "BARRETTO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_0)
    employee_value_1 = [
        {"EmployeeId": "2"},
        {"FirstName": "ARJAY"},
        {"LastName": "LOLONG"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_1)
    employee_value_2 = [
        {"EmployeeId": "3"},
        {"FirstName": "HENRY"},
        {"LastName": "CATALAN"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_2)
    employee_value_3 = [
        {"EmployeeId": "4"},
        {"FirstName": "JOHN PAUL"},
        {"LastName": "APIN"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_3)
    employee_value_4 = [
        {"EmployeeId": "5"},
        {"FirstName": "PHILIP"},
        {"LastName": "AREVALO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_4)
    employee_value_5 = [
        {"EmployeeId": "6"},
        {"FirstName": "JOHN CRIS"},
        {"LastName": "CARULLO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_5)
    employee_value_6 = [
        {"EmployeeId": "7"},
        {"FirstName": "JEROME"},
        {"LastName": "APIN"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_6)
    employee_value_7 = [
        {"EmployeeId": "8"},
        {"FirstName": "JOSHUA"},
        {"LastName": "SALAZAR"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_7)
    employee_value_8 = [
        {"EmployeeId": "9"},
        {"FirstName": "LOWEI JAY"},
        {"LastName": "AGUDO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_8)
    employee_value_9 = [
        {"EmployeeId": "10"},
        {"FirstName": "SALVADOR"},
        {"LastName": "SALAZAR"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_9)
    employee_value_10 = [
        {"EmployeeId": "11"},
        {"FirstName": "LARRY"},
        {"LastName": "CARULLO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_10)
    employee_value_11 = [
        {"EmployeeId": "12"},
        {"FirstName": "GREG"},
        {"LastName": "NEPOMUCENO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_11)
    employee_value_12 = [
        {"EmployeeId": "13"},
        {"FirstName": "JAIMIE"},
        {"LastName": "CONOCIDO"},
        {"MiddleName": ""},
        {"EmployeeDocuments": ""},
        {"MobileNumber": "(Mobile number not set)"},
        {"EmployeeAddress": "(Address not set)"}

    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_EmployeeList", employee_value_12)

    package_value_0 = [
        {"PackageId": "P_B"},
        {"PackageName": "Basic"},
        {"PackageDetails": "Wash,Armor All,Hand Wax,Under Wash"},
        {"PackagePriceSizeS": 1000},
        {"PackagePriceSizeM": 1100},
        {"PackagePriceSizeL": 1200},
        {"PackagePriceSizeXL": 1300},
        {"PackagePriceSizeXXL": 1600},
        {"PackageStatus": "Available"}
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_PackageList", package_value_0)
    package_value_1 = [
        {"PackageId": "P_ST"},
        {"PackageName": "Standard"},
        {"PackageDetails": "Wash,Armor All,Hand Wax,Under Wash,Glass Detailing"},
        {"PackagePriceSizeS": 2000},
        {"PackagePriceSizeM": 2200},
        {"PackagePriceSizeL": 2300},
        {"PackagePriceSizeXL": 2500},
        {"PackagePriceSizeXXL": 3000},
        {"PackageStatus": "Available"}
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_PackageList", package_value_1)
    package_values_2 = [
        {"PackageId": "P_D"},
        {"PackageName": "Deluxe"},
        {"PackageDetails": "Wash,Armor All,Asphalt Removal,Buff. Wax,Glass Detailing,BAC to Zero"},
        {"PackagePriceSizeS": 2600},
        {"PackagePriceSizeM": 2900},
        {"PackagePriceSizeL": 3100},
        {"PackagePriceSizeXL": 3300},
        {"PackagePriceSizeXXL": 4000},
        {"PackageStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_PackageList", package_values_2)
    package_values_3 = [
        {"PackageId": "P_U"},
        {"PackageName": "Ultimate"},
        {"PackageDetails": "Wash,Armor All,Asphalt Removal,Buff. Wax,Glass Detailing,Under Wash,BAC to Zero"},
        {"PackagePriceSizeS": 3000},
        {"PackagePriceSizeM": 3300},
        {"PackagePriceSizeL": 3600},
        {"PackagePriceSizeXL": 3900},
        {"PackagePriceSizeXXL": 4600},
        {"PackageStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_PackageList", package_values_3)
    package_values_4 = [
        {"PackageId": "P_SU"},
        {"PackageName": "Superior"},
        {"PackageDetails": "Wash,Armor All,3-Steps Buffing,Glass Detailing,Under Wash,BAC to Zero"},
        {"PackagePriceSizeS": 4500},
        {"PackagePriceSizeM": 5000},
        {"PackagePriceSizeL": 5500},
        {"PackagePriceSizeXL": 6000},
        {"PackagePriceSizeXXL": 7500},
        {"PackageStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_PackageList", package_values_4)

    service_values_0 = [
        {"ServiceId": "S_BWV"},
        {"ServiceName": "Body Wash and Vacuum"},
        {"ServicePriceSizeS": 140},
        {"ServicePriceSizeM": 160},
        {"ServicePriceSizeL": 180},
        {"ServicePriceSizeXL": 200},
        {"ServicePriceSizeXXL": 250},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_0)
    service_values_1 = [
        {"ServiceId": "S_AA"},
        {"ServiceName": "Armor All"},
        {"ServicePriceSizeS": 140},
        {"ServicePriceSizeM": 140},
        {"ServicePriceSizeL": 140},
        {"ServicePriceSizeXL": 140},
        {"ServicePriceSizeXXL": 200},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_1)
    service_values_2 = [
        {"ServiceId": "S_AR"},
        {"ServiceName": "Asphalt Removal"},
        {"ServicePriceSizeS": 200},
        {"ServicePriceSizeM": 230},
        {"ServicePriceSizeL": 250},
        {"ServicePriceSizeXL": 280},
        {"ServicePriceSizeXXL": 350},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_2)
    service_values_3 = [
        {"ServiceId": "S_SCCR"},
        {"ServiceName": "Seat Cover - Cloth - Removal"},
        {"ServicePriceSizeS": 150},
        {"ServicePriceSizeM": 200},
        {"ServicePriceSizeL": 250},
        {"ServicePriceSizeXL": 250},
        {"ServicePriceSizeXXL": 300},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_3)
    service_values_4 = [
        {"ServiceId": "S_SCCI"},
        {"ServiceName": "Seat Cover - Cloth - Install"},
        {"ServicePriceSizeS": 200},
        {"ServicePriceSizeM": 250},
        {"ServicePriceSizeL": 300},
        {"ServicePriceSizeXL": 350},
        {"ServicePriceSizeXXL": 350},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_4)
    service_values_5 = [
        {"ServiceId": "S_SCCL"},
        {"ServiceName": "Seat Cover - Cloth - Laundry"},
        {"ServicePriceSizeS": 150},
        {"ServicePriceSizeM": 150},
        {"ServicePriceSizeL": 200},
        {"ServicePriceSizeXL": 200},
        {"ServicePriceSizeXXL": 250},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_5)
    service_values_6 = [
        {"ServiceId": "S_SCLR"},
        {"ServiceName": "Seat Cover - Leather - Removal"},
        {"ServicePriceSizeS": 150},
        {"ServicePriceSizeM": 200},
        {"ServicePriceSizeL": 250},
        {"ServicePriceSizeXL": 250},
        {"ServicePriceSizeXXL": 300},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_6)
    service_values_7 = [
        {"ServiceId": "S_SCLI"},
        {"ServiceName": "Seat Cover - Leather - Install"},
        {"ServicePriceSizeS": 300},
        {"ServicePriceSizeM": 350},
        {"ServicePriceSizeL": 400},
        {"ServicePriceSizeXL": 400},
        {"ServicePriceSizeXXL": 450},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_7)
    service_values_8 = [
        {"ServiceId": "S_SCLL"},
        {"ServiceName": "Seat Cover - Leather - Laundry"},
        {"ServicePriceSizeS": 200},
        {"ServicePriceSizeM": 200},
        {"ServicePriceSizeL": 250},
        {"ServicePriceSizeXL": 250},
        {"ServicePriceSizeXXL": 300},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_8)
    service_values_9 = [
        {"ServiceId": "S_DBTZ"},
        {"ServiceName": "Disinfection (Bac to Zero)"},
        {"ServicePriceSizeS": 600},
        {"ServicePriceSizeM": 600},
        {"ServicePriceSizeL": 600},
        {"ServicePriceSizeXL": 600},
        {"ServicePriceSizeXXL": 600},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_9)
    service_values_10 = [
        {"ServiceId": "S_HC"},
        {"ServiceName": "Headlights Cleaning"},
        {"ServicePriceSizeS": 600},
        {"ServicePriceSizeM": 600},
        {"ServicePriceSizeL": 600},
        {"ServicePriceSizeXL": 600},
        {"ServicePriceSizeXXL": 600},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_10)
    service_values_11 = [
        {"ServiceId": "S_EW"},
        {"ServiceName": "Engine Wash"},
        {"ServicePriceSizeS": 700},
        {"ServicePriceSizeM": 700},
        {"ServicePriceSizeL": 700},
        {"ServicePriceSizeXL": 700},
        {"ServicePriceSizeXXL": 700},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_11)
    service_values_12 = [
        {"ServiceId": "S_MC"},
        {"ServiceName": "Muffler Cleaning"},
        {"ServicePriceSizeS": 500},
        {"ServicePriceSizeM": 600},
        {"ServicePriceSizeL": 700},
        {"ServicePriceSizeXL": 800},
        {"ServicePriceSizeXXL": 1000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_12)
    service_values_13 = [
        {"ServiceId": "S_DGD"},
        {"ServiceName": "Detailing - Glass Detailing"},
        {"ServicePriceSizeS": 1000},
        {"ServicePriceSizeM": 1100},
        {"ServicePriceSizeL": 1200},
        {"ServicePriceSizeXL": 1300},
        {"ServicePriceSizeXXL": 1600},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_13)
    service_values_14 = [
        {"ServiceId": "S_DB3S"},
        {"ServiceName": "Detailing - Buffing 3 Steps"},
        {"ServicePriceSizeS": 2700},
        {"ServicePriceSizeM": 3200},
        {"ServicePriceSizeL": 3700},
        {"ServicePriceSizeXL": 4200},
        {"ServicePriceSizeXXL": 5200},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_14)
    service_values_15 = [
        {"ServiceId": "S_DI"},
        {"ServiceName": "Detailing - Interior"},
        {"ServicePriceSizeS": 3500},
        {"ServicePriceSizeM": 4000},
        {"ServicePriceSizeL": 4500},
        {"ServicePriceSizeXL": 5000},
        {"ServicePriceSizeXXL": 6000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_15)
    service_values_16 = [
        {"ServiceId": "S_DE"},
        {"ServiceName": "Detailing - Exterior"},
        {"ServicePriceSizeS": 4000},
        {"ServicePriceSizeM": 4500},
        {"ServicePriceSizeL": 5000},
        {"ServicePriceSizeXL": 5500},
        {"ServicePriceSizeXXL": 6500},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_16)
    service_values_17 = [
        {"ServiceId": "S_DF"},
        {"ServiceName": "Detailing - Full"},
        {"ServicePriceSizeS": 7000},
        {"ServicePriceSizeM": 8000},
        {"ServicePriceSizeL": 9000},
        {"ServicePriceSizeXL": 10000},
        {"ServicePriceSizeXXL": 12000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_17)
    service_values_18 = [
        {"ServiceId": "S_U"},
        {"ServiceName": "Underwash"},
        {"ServicePriceSizeS": 400},
        {"ServicePriceSizeM": 450},
        {"ServicePriceSizeL": 500},
        {"ServicePriceSizeXL": 550},
        {"ServicePriceSizeXXL": 600},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_18)
    service_values_19 = [
        {"ServiceId": "S_UC"},
        {"ServiceName": "Under Coating"},
        {"ServicePriceSizeS": 6000},
        {"ServicePriceSizeM": 6500},
        {"ServicePriceSizeL": 7000},
        {"ServicePriceSizeXL": 7500},
        {"ServicePriceSizeXXL": 8500},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_19)
    service_values_20 = [
        {"ServiceId": "S_HWM"},
        {"ServiceName": "Hand Wax (Manual)"},
        {"ServicePriceSizeS": 500},
        {"ServicePriceSizeM": 550},
        {"ServicePriceSizeL": 600},
        {"ServicePriceSizeXL": 650},
        {"ServicePriceSizeXXL": 750},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_20)
    service_values_21 = [
        {"ServiceId": "S_BWM"},
        {"ServiceName": "Buff Wax (Machine)"},
        {"ServicePriceSizeS": 900},
        {"ServicePriceSizeM": 100},
        {"ServicePriceSizeL": 1100},
        {"ServicePriceSizeXL": 1200},
        {"ServicePriceSizeXXL": 1500},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_21)
    service_values_22 = [
        {"ServiceId": "S_CCBP"},
        {"ServiceName": "Ceramic Coating - Body Panels"},
        {"ServicePriceSizeS": 15000},
        {"ServicePriceSizeM": 16000},
        {"ServicePriceSizeL": 17000},
        {"ServicePriceSizeXL": 18000},
        {"ServicePriceSizeXXL": 20000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_22)
    service_values_23 = [
        {"ServiceId": "S_CCG"},
        {"ServiceName": "Ceramic Coating - Glass"},
        {"ServicePriceSizeS": 5000},
        {"ServicePriceSizeM": 6000},
        {"ServicePriceSizeL": 7000},
        {"ServicePriceSizeXL": 8000},
        {"ServicePriceSizeXXL": 10000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_23)
    service_values_24 = [
        {"ServiceId": "S_CCR"},
        {"ServiceName": "Ceramic Coating - Rims"},
        {"ServicePriceSizeS": 3000},
        {"ServicePriceSizeM": 4000},
        {"ServicePriceSizeL": 5000},
        {"ServicePriceSizeXL": 6000},
        {"ServicePriceSizeXXL": 6000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_24)
    service_values_25 = [
        {"ServiceId": "S_RBWWOH"},
        {"ServiceName": "Repainting/Body Works - Wash Over (Hilamos)"},
        {"ServicePriceSizeS": 30000},
        {"ServicePriceSizeM": 35000},
        {"ServicePriceSizeL": 40000},
        {"ServicePriceSizeXL": 45000},
        {"ServicePriceSizeXXL": 50000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_25)
    service_values_26 = [
        {"ServiceId": "S_RBWPPPSA"},
        {"ServiceName": "Repainting/Body Works - Per Panel Price Starts At"},
        {"ServicePriceSizeS": 3500},
        {"ServicePriceSizeM": 3500},
        {"ServicePriceSizeL": 4000},
        {"ServicePriceSizeXL": 4500},
        {"ServicePriceSizeXXL": 5000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_26)
    service_values_27 = [
        {"ServiceId": "S_RBWRC"},
        {"ServiceName": "Repainting/Body Works - Rims - Car"},
        {"ServicePriceSizeS": 5000},
        {"ServicePriceSizeM": 5000},
        {"ServicePriceSizeL": 6000},
        {"ServicePriceSizeXL": 8000},
        {"ServicePriceSizeXXL": 8000},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_27)
    service_values_28 = [
        {"ServiceId": "S_VCBW"},
        {"ServiceName": "Body Wash"},
        {"ServicePriceSizeS": 120},
        {"ServicePriceSizeM": 150},
        {"ServicePriceSizeL": 200},
        {"ServicePriceSizeXL": -1},
        {"ServicePriceSizeXXL": -1},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_28)
    service_values_29 = [
        {"ServiceId": "S_VCBW"},
        {"ServiceName": "Body Wash - Tricycle"},
        {"ServicePriceSizeS": -1},
        {"ServicePriceSizeM": 220},
        {"ServicePriceSizeL": -1},
        {"ServicePriceSizeXL": -1},
        {"ServicePriceSizeXXL": -1},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_29)
    service_values_30 = [
        {"ServiceId": "S_VCBWP"},
        {"ServiceName": "Body Wash - PUV"},
        {"ServicePriceSizeS": -1},
        {"ServicePriceSizeM": 400},
        {"ServicePriceSizeL": -1},
        {"ServicePriceSizeXL": -1},
        {"ServicePriceSizeXXL": -1},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_30)
    service_values_31 = [
        {"ServiceId": "S_VCA"},
        {"ServiceName": "Armor"},
        {"ServicePriceSizeS": 100},
        {"ServicePriceSizeM": 100},
        {"ServicePriceSizeL": 100},
        {"ServicePriceSizeXL": -1},
        {"ServicePriceSizeXXL": -1},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_31)
    service_values_32 = [
        {"ServiceId": "S_VCW"},
        {"ServiceName": "Wax (Manual)"},
        {"ServicePriceSizeS": 150},
        {"ServicePriceSizeM": 150},
        {"ServicePriceSizeL": 150},
        {"ServicePriceSizeXL": -1},
        {"ServicePriceSizeXXL": -1},
        {"ServiceStatus": "Available"},
    ]
    a.insertToTable("AUTOLANDIA", "AUTOLANDIA_ServiceList", service_values_32)