class Activity extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
        <div id="activity">
            <section>
                <img src="./images/activity/Group 70.png">
                <div>
                    <h2>Inauguration of New Elected SRC executives</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
            <section>
                <img src="./images/activity/Group 71.png">
                <div>
                    <h2>The fifth Edo State University Founders day</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
        </div>
        <div id="activity">
            <section>
                <img src="./images/activity/Group 72.png">
                <div>
                    <h2>Deadline For Course Registration </h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
            <section>
                <img src="./images/activity/Group 73.png">
                <div>
                    <h2>Students Week</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
        </div>
        <div id="activity">
            <section>
                <img src="./images/activity/Group 74.png">
                <div>
                    <h2>Third Convocation Ceremony</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
            <section>
                <img src="./images/activity/Group 75.png">
                <div>
                    <h2>Business Implementation Seminar for Final Year</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur
                        <br> adipiscing elit, sed do eiusmod tempor 
                        <br> incididunt consequat. Duis aute irure dolor in 
                        <br> reprehenderit in voluptate velit esse cillum dolore eu 
                        <br><b>Continue Reading...</b></p>
                </div>
            </section>
        </div>

        <button>Load More</button>
        `
    }
}

customElements.define('app-activity', Activity);