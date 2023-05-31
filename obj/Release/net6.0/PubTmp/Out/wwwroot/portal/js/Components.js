class Newsletter extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
            <div class="newsletter">
                <div class="newsletter-container">
                    <div class="newsletter-text">
                        <h1>Don't miss our weekly updates about academics information!</h1>
                    </div>
                    <div class="email">
                        <form action="#">
                            <div class="newsletter-input">
                                <input type="email" name="" id="" placeholder="Enter your email address">
                                <button>SUBSCRIBE</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        `
    }
}

customElements.define('app-newsletter', Newsletter);