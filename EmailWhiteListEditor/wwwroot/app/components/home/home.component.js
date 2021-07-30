"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const home_service_1 = require("../../core/services/home.service");
let HomeComponent = class HomeComponent {
    constructor(hserv) {
        this.hserv = hserv;
        this.result = [];
        this.backup = [];
        this.keyword = '';
        this.init();
    }
    ngOnInit() {
    }
    init() {
        this.hserv.GetAllEntries().subscribe(response => this.copyToBackup(this.result = response));
    }
    copyToBackup(arr) {
        this.backup = JSON.parse(JSON.stringify(arr));
    }
    add() {
        if (!this.check()) {
            alert('The word is not valid. ');
            return;
        }
        else {
            this.hserv.AddEntry(this.keyword).subscribe(response => this.copyToBackup(this.result = response));
        }
    }
    edit(index) {
        let key = this.backup[index];
        let value = this.result[index];
        console.log(value);
        this.hserv.EditEntry(key, value)
            .subscribe(response => {
            this.copyToBackup(this.result = response);
            this.init();
        });
    }
    delete(key, index) {
        let originalValue = this.backup[index];
        console.log('deleting for: ' + originalValue);
        if (confirm(`Would you like to delete '${originalValue}' ?`)) {
            this.hserv.DeleteEntry(originalValue).subscribe(response => this.copyToBackup(this.result = response));
            // TODO: notify success full message
        }
        else {
            this.result[index] = this.backup[index];
        }
    }
    handleKeyUp(e) {
        this.keyword = this.keyword || '';
        // if(e.keyCode === 13)
        if (this.keyword.length > 2) {
            this.hserv.GetEntries(this.keyword).subscribe(response => this.copyToBackup(this.result = response));
        }
        console.log("typed with " + this.keyword);
    }
    check() {
        debugger;
        // if ( this.keyword == "")
        // {
        // return;
        // }
        // if ( !this.keyword.includes("@"))
        // {
        // return;
        // }
        // if ( !this.keyword.includes("."))
        // {
        // return;
        // }
        /// ^w+[+.w-]*@([w-]+.)*w+[w-]*.([a-z]{2,4}|d+)$/i
        // (([w-]+.)*w+[w-]*.)
        //let formatAddress : RegExp = /^w+[+.w-]*@([w-]+.)*w+[w-]*.([a-z]{2,4}|d+)$/i
        let formatAddress = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
        let formatdomain = /^[^@\s]+\.[^@\s]+$/;
        if (formatAddress.test(this.keyword)
            ||
                formatdomain.test(this.keyword)) {
            return true;
        }
        else {
            return false;
        }
    }
    trackByFn(index, item) {
        return index;
    }
};
HomeComponent = __decorate([
    core_1.Component({
        selector: 'home',
        templateUrl: 'home.component.html'
    }),
    __metadata("design:paramtypes", [home_service_1.HomeService])
], HomeComponent);
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map