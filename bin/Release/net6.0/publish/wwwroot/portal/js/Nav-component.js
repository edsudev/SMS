class Navigatione extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
            <nav class="nav-desktop">
                <a href="/">
                    <div class="logo">
                        <img src="../../portal/images/nav/logo 1.png" alt="">
                        <h3>
                            Edo State <br> University Uzairue
                        </h3>
                    </div>
                </a>
                <ul>
                <li class="nav-item">
                    <a class="" href="/">
                        Home
                    </a>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            About
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/home/about">About-Us</a>
                            <a class="dropdown-item" href="/home/programs">Programs</a>
                            <a class="dropdown-item" href="/home/contact">Contact-Us</a>
                        </div>
                    </div>
                </li>

                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Academics
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="#">Applied Health Science</a>
                            <a class="dropdown-item" href="#">Art Management & Social Sciences</a>
                            <a class="dropdown-item" href="#">Basic Medical Science</a>
                            <a class="dropdown-item" href="#">Clinical Science</a>
                            <a class="dropdown-item" href="#">Engineering</a>
                            <a class="dropdown-item" href="#">Law</a>
                            <a class="dropdown-item" href="#">Science</a>
                            
                        </div>
                    </div>
                </li>

                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Admissions
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">

                            <a class="dropdown-item" href="/admissions/undergraduate">Undergraduate</a>
                            <a class="dropdown-item" href="/pgadmissions/postgraduate">Post Graduate</a>
                            <a class="dropdown-item" href="/conversionadmissions/conversion">Conversion</a>
                            <a class="dropdown-item" href="/jupebadmission/jupeb">JUPEB</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Vacancies
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/vacancies/academic">Academic</a>
                            <a class="dropdown-item" href="/vacancies/nonacademic">Non Academic</a>
                            <a class="dropdown-item" href="/entreprenureships/create">Entrepreunership</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Media
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/home/activities">Activity</a>
                            <a class="dropdown-item" href="/home/news">Campus News</a>
                            <a class="dropdown-item" href="/home/magazines">EDSU Magazine</a>
                            <a class="dropdown-item" href="/home/events">Event Gallery</a>
                            <a class="dropdown-item" href="/home/gallery">Building Gallery</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            OER
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/oerconferencepapers/index">Conference Papers</a>
                            <a class="dropdown-item" href="/oercoursewares/index">Coursewares</a>
                            <a class="dropdown-item" href="/oerjournalarticles/index">Journal Articles</a>
                            <a class="dropdown-item" href="/oerstudentprojects/index">Students Projects</a>
                            <a class="dropdown-item" href="/oertextbooks/index">Textbooks</a>
                        </div>
                    </div>
                </li>
                 <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="https://odl.edouniversity.edu.ng" role="button">
                            ODL
                        </a>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="https://edsuth.edouniversity.edu.ng"role="button">
                            EDSUTH
                        </a>
                    </div>
                </li>
        
            </ul>
            </nav>

            <nav class="nav-mobile">
               <div class="d-flex" style="justify-content: space-between">
                    <div class="first-nav">
                        <a href="index.html">
                            <div class="logo">
                                <img src="../../portal/images/nav/logo 1.png" alt="">
                                <h3>
                                    Edo State <br> University Uzairue
                                </h3>
                            </div>
                        </a>
                    
                    </div>
                    <div class="hamburger">
                        <div class="hamburger-line"></div>
                        <div class="hamburger-line"></div>
                        <div class="hamburger-line"></div>
                    </div>
               </div>
                
                <div class="mobile-menu">
                 <ul>
                <li class="nav-item">
                    <a class="" href="/">
                        Home
                    </a>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            About
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/home/about">About-Us</a>
                            <a class="dropdown-item" href="/home/programs">Programs</a>
                            <a class="dropdown-item" href="/home/contact">Contact-Us</a>
                        </div>
                    </div>
                </li>

                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Academics
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="#">Applied Health Science</a>
                            <a class="dropdown-item" href="#">Art Management & Social Sciences</a>
                            <a class="dropdown-item" href="#">Basic Medical Science</a>
                            <a class="dropdown-item" href="#">Clinical Science</a>
                            <a class="dropdown-item" href="#">Engineering</a>
                            <a class="dropdown-item" href="#">Law</a>
                            <a class="dropdown-item" href="#">Science</a>
                            
                        </div>
                    </div>
                </li>

                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Admissions
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">

                            <a class="dropdown-item" href="/admissions/undergraduate">Undergraduate</a>
                            <a class="dropdown-item" href="/pgadmissions/postgraduate">Post Graduate</a>
                            <a class="dropdown-item" href="/conversionadmissions/conversion">Conversion</a>
                            <a class="dropdown-item" href="/jupebadmission/jupeb">JUPEB</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Vacancies
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/vacancies/academic">Academic</a>
                            <a class="dropdown-item" href="/vacancies/nonacademic">Non Academic</a>
                            <a class="dropdown-item" href="/entreprenureships/create">Entrepreunership</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Media
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/home/activities">Activity</a>
                            <a class="dropdown-item" href="/home/news">Campus News</a>
                            <a class="dropdown-item" href="/home/magazines">EDSU Magazine</a>
                            <a class="dropdown-item" href="/home/events">Event Gallery</a>
                            <a class="dropdown-item" href="/home/gallery">Building Gallery</a>
                        </div>
                    </div>
                </li>
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink7" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            OER
                            <i data-feather="chevron-down"></i>
                        </a>

                        <div class="dropdown-menu" aria-labelledby="dropdownMenuLink7">
                            <a class="dropdown-item" href="/oerconferencepapers/index">Conference Papers</a>
                            <a class="dropdown-item" href="/oercoursewares/index">Coursewares</a>
                            <a class="dropdown-item" href="/oerjournalarticles/index">Journal Articles</a>
                            <a class="dropdown-item" href="/oerstudentprojects/index">Students Projects</a>
                            <a class="dropdown-item" href="/oertextbooks/index">Textbooks</a>
                        </div>
                    </div>
                </li>
                
                <li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="https://odl.edouniversity.edu.ng" role="button">
                            ODL
                        </a>
                    </div>
                </li>
<li class="nav-item">
                    <div class="dropdown nav-link ">
                        <a class="dropdown-toggle" href="https://edsuth.edouniversity.edu.ng" >
                            EDSUTH
                        </a>
                    </div>
                </li>
        
            </ul>
            </div>
            </nav>
        `;
        
        // Dropdown functionality
        //const dropdowns = this.querySelectorAll('.dropdown');

        //dropdowns.forEach((dropdown) => {
        //    const button = dropdown.querySelector('.dropdown-btn');
        //    const content = dropdown.querySelector('.dropdown-content');

        //    button.addEventListener('click', () => {
        //        content.classList.toggle('show');
        //    });
        //});

        const hamburger = this.querySelector('.hamburger');
        const mobileMenu = this.querySelector('.mobile-menu');

        hamburger.addEventListener('click', () => {
            mobileMenu.classList.toggle('active');
        });
    }
}

customElements.define('app-navigaton', Navigatione);
