class Searchbox extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
        <div id="filter">
        <div class="custom-select" style="width:200px;">
            <select>
              <option value="0">Faculty</option>
              <option value="1">Faculty of Applied Health Science</option>
              <option value="2">Faculty of Arts, Management and Social Sciences</option>
              <option value="3">College of Basic Medical Sciences</option>
              <option value="4">College of Clinical Sciences</option>
              <option value="5">Faculty of Engineering</option>
              <option value="6">Faculty of Law</option>
              <option value="7">Faculty of Science</option>
          </select>
        </div>
        <img id="img" src="~/portal/images/programs/icons/separator.png"/>
        <div class="custom-select" style="width:200px;">
            <select>
              <option value="0">Title</option>
              <option value="1">Bachelor's Degree</option>
              <option value="2">Conversion Program</option>
              <option value="3">Masters Degree</option>
          </select>
        </div>
        <img id="img" src="~/portal/images/programs/icons/separator.png"/>
        <span>
            <input type="search" placeholder="Search">
            <img class="img" src="~/portal/images/programs/icons/search.png">
        </span>
    </div>
        `
    }
}

customElements.define('app-searchbox', Searchbox);