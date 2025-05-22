SELECT 
            u.Name,
            u.Surname,
            a.appID
            
        FROM
            Appointments a
        JOIN 
            Users u ON a.User_ID = u.User_ID
        WHERE
        a.appID = 13