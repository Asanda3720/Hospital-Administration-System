<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMeals.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Meal.OrderMeals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Meals - MediConnect</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    
    <style type="text/css">
        :root {
            --primary-color: #2a9d8f;
            --secondary-color: #264653;
            --accent-color: #e9c46a;
            --light-color: #f8f9fa;
            --dark-color: #212529;
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            color: var(--dark-color);
            background-color: var(--light-color);
            line-height: 1.6;
        }
        
        /* Header Styles */
        .top-info-bar {
            background-color: var(--secondary-color);
            color: white;
            padding: 0.5rem 0;
            font-size: 0.9rem;
        }

        .top-info-content {
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 1rem;
        }

        .contact-info a {
            color: white;
            text-decoration: none;
            margin-right: 1.5rem;
        }

        .contact-info a:hover {
            color: var(--accent-color);
        }

        .main-header {
            background-color: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .navbar-brand {
            font-size: 1.8rem;
            font-weight: 700;
            color: var(--secondary-color);
        }

        .navbar-brand span {
            color: var(--primary-color);
        }

        .navbar-nav .nav-link {
            color: var(--secondary-color);
            font-weight: 500;
            padding: 0.5rem 1rem;
            margin: 0 0.25rem;
            border-radius: 4px;
        }

        .navbar-nav .nav-link:hover,
        .navbar-nav .nav-link.active {
            color: white;
            background-color: var(--primary-color);
        }

        .user-email {
            color: var(--accent-color);
            font-weight: 500;
            margin-left: 1rem;
        }

        /* Main Content Styles */
        .meal-container {
            max-width: 800px;
            margin: 2rem auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        .meal-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            text-align: center;
        }

        .meal-title span {
            color: var(--primary-color);
        }

.meal-section-div {
    display: flex;
    flex-wrap: wrap; /* Allow wrapping on smaller screens */
    gap: 1rem; /* Spacing between items */
    margin-bottom: 1.5rem;
}

.meal-section h4 {
    color: var(--secondary-color);
    margin-bottom: 0.5rem;
}

.meal-section-div {
    display: flex;
    /*flex-wrap: wrap; /* Allow wrapping on smaller screens */
    gap: 1rem; /* Spacing between items */
    margin-bottom: 1.5rem;
}

.meal-section h4 {
    color: var(--secondary-color);
    margin-bottom: 0.5rem;
}

.meal-section-div-title {
    display: flex;
    flex-direction: column; /* Stack name & price vertically */
    justify-content: center;
    font-weight: bold;
}

.meal-section {
    display: flex;
    /**/flex-direction: row;*/
    align-items: center;
    gap: 16px;
    background-color: red; /* You can change this to a lighter tone */
    /*width: 28%;*/
    min-width: 250px; /* Ensures a reasonable min width */
    height: 150px; /* Adjust as needed */
    border: 2px solid #ccc;
    border-radius: 10px;
    padding: 10px;
    box-shadow: 0 2px 5px rgba(0,0,0,0.1);
    cursor: pointer;
    transition: transform 0.2s ease;
}

.meal-section:hover {
    transform: scale(1.03);
    background-color: #f2f2f2;
}

.meal-section-div-img img {
    width: 100px;
    height: 100px;
    object-fit: cover;
    border-radius: 8px;
}

        .form-control {
            width: 100%;
            padding: 0.75rem 1rem;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
            transition: all 0.3s;
        }

        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.2rem rgba(42, 157, 143, 0.25);
            outline: none;
        }

        .btn-order {
            background-color:  #2a9d8f;
            color: white;
            border: none;
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s;
            display: block;
            width: 100%;
            max-width: 200px;
            margin: 2rem auto 0;
        }

        .btn-order:hover {
            background-color: #21867a;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }
        .meal-section.selected {
    border-color: #007bff;
    background-color: #e0f0ff;
}

.form-footer {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    margin-top: 20px;
    gap: 20px;
}

.total-display {
    font-size: 1.2rem;
    font-weight: bold;
}

.meal-section-div {
    display: flex;
    gap: 1rem;
    overflow-x: auto;
    padding-bottom: 1rem;
}

        /* Footer Styles */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 2rem 0;
            text-align: center;
            margin-top: 2rem;
        }

        .footer-links {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 1.5rem;
            margin-bottom: 1.5rem;
        }

        .footer-links a {
            color: white;
            text-decoration: none;
        }

        .footer-links a:hover {
            color: var(--accent-color);
        }

        .copyright {
            font-size: 0.9rem;
            opacity: 0.8;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .top-info-content {
                justify-content: center;
                text-align: center;
            }
            
            .contact-info a {
                display: block;
                margin: 0 0 0.5rem 0;
            }
            
            .meal-container {
                padding: 1.5rem;
                margin: 1rem;
            }
        }
        .meal-container {
    padding: 20px;
    font-family: Arial, sans-serif;
}

.meal-title {
    text-align: center;
    font-size: 2rem;
    margin-bottom: 2rem;
}

h4 {
    margin-top: 1.5rem;
    margin-bottom: 0.5rem;
    color: #444;
}

.meal-section-div {
    display: flex;
    gap: 1rem;
    margin-bottom: 2rem;
    padding-bottom: 1rem;
    overflow-x: auto;
    scrollbar-width: thin;
    scrollbar-color: #999 #eee;
}

.meal-section-div::-webkit-scrollbar {
    height: 8px;
}

.meal-section-div::-webkit-scrollbar-track {
    background: #eee;
    border-radius: 10px;
}

.meal-section-div::-webkit-scrollbar-thumb {
    background-color: #ccc;
    border-radius: 10px;
}

.meal-section {
    min-width: 250px;
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 16px;
    background-color: #f8f8f8;
    border: 2px solid #ddd;
    border-radius: 10px;
    padding: 10px;
    box-sizing: border-box;
    transition: transform 0.2s ease;
    cursor: pointer;
    flex-shrink: 0;
}

.meal-section:hover {
    transform: scale(1.03);
    background-color: #f0f0f0;
}

.meal-section-div-img img {
    width: 80px;
    height: 80px;
    object-fit: cover;
    border-radius: 8px;
}

.meal-section-div-title {
    display: flex;
    flex-direction: column;
    justify-content: center;
    font-weight: bold;
    font-size: 1rem;
}

.btn-order {
    margin-top: 20px;
    background-color:var(--secondary-color);
    color: white;
    padding: 12px 20px;
    border: none;
    border-radius: 6px;
    font-size: 1rem;
    cursor: pointer;
}

.btn-order:hover {
    opacity:80%;

}
.meal-section.selected {
    border-color: var(--secondary-color);
    background-color: #e0f0ff;
}

.form-footer {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    margin-top: 20px;
    gap: 20px;
}

.total-display {
    font-size: 1.2rem;
    font-weight: bold;
}

.meal-section-div {
    display: flex;
    gap: 1rem;
    overflow-x: auto;
    padding-bottom: 1rem;
}
.meal-section-div {
    display: flex;
    flex-direction: row;
    overflow-x: auto;
    overflow-y: hidden;
    white-space: nowrap;
    gap: 1rem;
    padding: 10px 0;
    scroll-behavior: smooth;
}

.meal-section-div::-webkit-scrollbar {
    height: 8px;
}

.meal-section-div::-webkit-scrollbar-thumb {
    background-color: #ccc;
    border-radius: 10px;
}

.meal-section {
    flex: 0 0 auto;
    width: 250px;
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 10px;
    box-sizing: border-box;
    background-color: #f9f9f9;
    transition: transform 0.2s;
}

.meal-section:hover {
    transform: scale(1.03);
    background-color: #eef6ff;
}

.meal-section-div-img img {
    width: 100%;
    height: 100px;
    object-fit: cover;
    border-radius: 6px;
}

.meal-section-div-title {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 8px;
    font-weight: bold;
}
.meal-section-div {
    max-width: 100%; /* or 900px */
}


    </style>
</head>
<body>
    <!-- Top Info Bar -->
    <div class="top-info-bar">
        <div class="container">
            <div class="top-info-content">
                <div class="contact-info">
                    <a href="tel:+1234567890"><i class="fas fa-phone-alt"></i> +1 (234) 567-890</a>
                    <a href="mailto:info@mediconnect.com"><i class="fas fa-envelope"></i> info@mediconnect.com</a>
                </div>
                                    <div>
    <%
        HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
        if (userCookieRetriever != null)
        {
            string email = userCookieRetriever["email"];
            Response.Write("<a href='/Web_Forms/Account/LogOut.aspx' class='user-email'><i class='fas fa-user-circle'></i> " + email + "</a>");
        }
        else
        {
            Response.Write("<a href='/Web_Forms/Login.aspx' class='login-link'><i class='fas fa-user'></i> Login</a>");
        }
    %>
</div>
            </div>
        </div>
    </div>

    <!-- Main Navigation -->
    <header class="main-header">
        <nav class="navbar navbar-expand-lg navbar-light">
            <div class="container">
                <a class="navbar-brand" href="/Web_Forms/Home.aspx">Medi<span>Connect</span></a>
                
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" 
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                       <li class="nav-item">
    <a class="nav-link active" href="/Web_Forms/Home.aspx">HOME</a>
</li>
                                                        <%
                                //HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "1")
                                    {

                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/DirectAppointmentBookings.aspx'>APPOINTMENT</a>");
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Emergency/RequestEmergency.aspx'>EMERGENCY</a>");
                                    }
                                }
                                
%>
<li class="nav-item">
    <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
</li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

<!-- Main Content -->
<!-- Main Content -->
<form id="form1" runat="server">
    <div class="meal-container">
        <h1 class="meal-title">Order <span>Meals</span></h1>

        <h4 id="lblErrorMeal" runat="server" visible="false" style="color: red;">At least 1 meal must be chosen!</h4>

        <!-- BREAKFAST -->
        <h4>For Breakfast:</h4>
        <div class="meal-section-div scroll-x">
            <div class="meal-section" data-price="50" data-name="Eggs and Toast">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/pics/breakfast/EggswithToast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Eggs and Toast</div>
                    <div>R50</div>
                </div>
            </div>

            <div class="meal-section" data-price="70" data-name="Oats & Fruit">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/pics/breakfast/EggswithToast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Oats & Fruit</div>
                    <div>R70</div>
                </div>
            </div>

            <div class="meal-section" data-price="85" data-name="Pancakes">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/pics/breakfast/Toast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Pancakes</div>
                    <div>R85</div>
                </div>
            </div>
        </div>

        <!-- LUNCH -->
        <h4>For Lunch:</h4>
        <div class="meal-section-div scroll-x">
            <div class="meal-section" data-price="120" data-name="Grilled Chicken">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/images/Lunch/WhatsApp Image 2025-05-24 at 18.35.08.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Grilled Chicken</div>
                    <div>R120</div>
                </div>
            </div>

            <div class="meal-section" data-price="95" data-name="Veggie Bowl">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/images/Lunch/WhatsApp Image 2025-05-24 at 18.35.08.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Veggie Bowl</div>
                    <div>R95</div>
                </div>
            </div>

        
            <div class="meal-section" data-price="110" data-name="Beef Wraps">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/pics/breakfast/EggswithToast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Beef Wraps</div>
                    <div>R110</div>
                </div>
            </div>
        </div>
        
        <!-- DINNER -->
        <h4>For Evening:</h4>
        <div class="meal-section-div scroll-x">
            <div class="meal-section" data-price="100" data-name="Pasta">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/images/Breakfast/EggswithToast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Pasta</div>
                    <div>R100</div>
                </div>
            </div>
             <div class="meal-section" data-price="130" data-name="Grilled Fish">
     <div class="meal-section-div-img">
         <img src="/Web_Forms/Meal/images/Supper/roast-beef-with-mashed-potatoes.jpeg" />
     </div>
     <div class="meal-section-div-title">
         <div>Grilled Fish</div>
         <div>R130</div>
     </div>
    </div>

            <div class="meal-section" data-price="95" data-name="Chicken Salad">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/images/Breakfast/roast chicken with mashed potato.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Chicken Mash</div>
                    <div>R95</div>
                </div>
            </div>
              <div class="meal-section" data-price="55" data-name="Chicken Salad">
                <div class="meal-section-div-img">
                    <img src="/Web_Forms/Meal/images/Breakfast/EggswithToast.jpeg" />
                </div>
                <div class="meal-section-div-title">
                    <div>Chicken Salad</div>
                    <div>R55</div>
                </div>
            </div>
        </div>

        <!-- Total & Submit -->
        <div class="form-footer">
            <asp:HiddenField ID="selectedMeals" runat="server" />
            <span class="total-display">Total: R<span id="totalAmount">0</span></span>
            <asp:Button ID="btnConfirm" runat="server" Text="Order Meals" CssClass="btn-order" OnClick="btnConfirm_Click" />
            <input type="hidden" id="totalAmountInput" name="totalAmountInput" />
        </div>
    </div>
</form>






    <!-- Footer -->
    <footer class="footer">
        <div class="container">
            <div class="footer-links">
                <a href="/About">About Us</a>
                <a href="/Services">Services</a>
                <a href="/Contact">Contact</a>
                <a href="/Privacy">Privacy Policy</a>
                <a href="/Terms">Terms of Service</a>
            </div>
            <div class="social-icons mb-3">
                <a href="#" class="text-white mx-2"><i class="fab fa-facebook-f"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-twitter"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-instagram"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-linkedin-in"></i></a>
            </div>
            <div class="copyright">
                &copy; <script>document.write(new Date().getFullYear())</script> MediConnect. All rights reserved.
            </div>
        </div>
    </footer>

    <script type="text/javascript">
        let total = 0;

        document.querySelectorAll(".meal-section").forEach(card => {
            card.addEventListener("click", () => {
                card.classList.toggle("selected");

                // Recalculate total
                const selectedCards = document.querySelectorAll(".meal-section.selected");
                total = 0;
                selectedCards.forEach(item => {
                    total += parseInt(item.getAttribute("data-price") || 0);
                });

                // Set hidden field value and update UI
                document.getElementById("totalAmountInput").value = total;
                document.getElementById("total-display").innerText = "Total: R" + total;
            });
        });
    </script>




    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>