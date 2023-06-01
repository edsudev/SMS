class Footer extends HTMLElement{
    connectedCallback(){
        this.innerHTML = `
            <footer class="footer">
                <div class="footer-container">
                    <div class="footer-details">
                        <div class="logo">
                            <img src="images/nav/logo 1.png" alt="">
                            <h3>
                                Edo State <br> University Uzairue
                            </h3>
                        </div>
                        <p>
                            Edo State University, Uzuaire, lorem ipsum dolor sit met, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
                        </p>
                        <div class="icons">
                            <a class="box" href="">
                                <i class="fa-brands fa-facebook-f "></i>
                            </a>
                            <a class="box" href="">
                                <i class="fa-brands fa-youtube "></i>
                            </a>
                            <a class="box" href="">
                                <i class="fa-brands fa-twitter "></i>
                            </a>
                            <a class="box" href="">
                                <i class="fa-brands fa-linkedin "></i>
                            </a>
                            <a class="box" href="">
                                <i class="fa-brands fa-instagram "></i>
                            </a>
                        </div>
                    </div>

                    <div class="list-links">
                        <h3>
                            EDSU
                        </h3>
                        <a href="">
                            Why EDSU
                        </a>
                        <a href="">
                            Programs
                        </a>
                        <a href="">
                            Scholarship
                        </a>
                        <a href="">
                            Security
                        </a>
                        <a href="">
                            Tuition
                        </a>
                        <a href="">
                            Fees
                        </a>
                    </div>

                    <div class="list-links">
                        <h3>
                            Resources
                        </h3>
                        <a href="">
                            Download
                        </a>
                        <a href="">
                            Help Center
                        </a>
                        <a href="">
                            Events
                        </a>
                        <a href="">
                            Guides
                        </a>
                        <a href="">
                            Partner
                        </a>
                        <a href="">
                            Directories
                        </a>
                    </div>

                    <div class="list-links">
                        <h3>
                            Community
                        </h3>
                        <a href="">
                            About us
                        </a>
                        <a href="">
                            Contact us
                        </a>
                        <a href="">
                            Academics
                        </a>
                        <a href="">
                            Register
                        </a>
                        <a href="">
                            LMS
                        </a>
                        <a href="">
                            FAQ
                        </a>
                    </div>

                    <div class="link-mobile">
                            <div class="list-link">
                            <h3>
                                EDSU
                            </h3>
                            <a href="">
                                Why EDSU
                            </a>
                            <a href="">
                                Programs
                            </a>
                            <a href="">
                                Scholarship
                            </a>
                            <a href="">
                                Security
                            </a>
                            <a href="">
                                Tuition
                            </a>
                            <a href="">
                                Fees
                            </a>
                        </div>

                        <div class="list-link">
                            <h3>
                                Resources
                            </h3>
                            <a href="">
                                Download
                            </a>
                            <a href="">
                                Help Center
                            </a>
                            <a href="">
                                Events
                            </a>
                            <a href="">
                                Guides
                            </a>
                            <a href="">
                                Partner
                            </a>
                            <a href="">
                                Directories
                            </a>
                        </div>

                        <div class="list-link">
                            <h3>
                                Community
                            </h3>
                            <a href="">
                                About us
                            </a>
                            <a href="">
                                Contact us
                            </a>
                            <a href="">
                                Academics
                            </a>
                            <a href="">
                                Register
                            </a>
                            <a href="">
                                LMS
                            </a>
                            <a href="">
                                FAQ
                            </a>
                        </div>
                    </div>

                    <div class="address">
                        <h3>
                            Get in Touch with Us
                        </h3>
                        <div class="icon-box">
                            <div class="icon">
                                <i class="fa-solid fa-location-dot"></i>
                            </div>
                            <p class="details poppins">
                                Km7, Auchi-Abuja Road, <br> Iyamho-Uzairue Edo State, Nigeria
                            </p>
                        </div>
                        <div class="icon-box">
                            <div class="icon">
                                <i class="fa-solid fa-phone"></i>
                            </div>
                            <p class="details poppins">
                                08064888518,08065528915, 09025412171, 07012641913
                            </p>
                        </div>
                        <div class="icon-box">
                            <div class="icon">
                                <i class="fa-regular fa-envelope"></i>
                            </div>
                            <p class="details poppins">
                                registra@edouniversity.edu.ng
                            </p>
                        </div>
                    </div>
                </div>
                <p class="copyright">
                    Edo State University Uzairue - &copy; 2022 All Rights Reserved.
                </p>
            </footer>
        `
    }
}

customElements.define('app-footer',Footer)