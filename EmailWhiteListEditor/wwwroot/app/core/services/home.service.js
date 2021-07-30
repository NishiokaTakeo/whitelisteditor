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
const http_1 = require("@angular/http");
const rxjs_1 = require("rxjs");
let HomeService = class HomeService {
    constructor(http) {
        this.http = http;
    }
    GetHomeMessage() {
        return this.http.get(`api/default`)
            .map((res) => res.json())
            .catch((error) => rxjs_1.Observable.throw(error.json().error || 'Server error'));
    }
    GetEntries(keyword) {
        return this.http.post(`api/default/entries/` + keyword, "")
            .map((res) => {
            return res.json();
        })
            .catch((error) => rxjs_1.Observable.throw(error.json().error || 'Server error'));
    }
    GetAllEntries() {
        return this.http.post(`api/default/entries/all`, "")
            .map((res) => {
            return res.json();
        })
            .catch((error) => rxjs_1.Observable.throw(error.json().error || 'Server error'));
    }
    AddEntry(keyword) {
        return this.http.post(`api/default/add/` + keyword, {})
            .map((res) => res.json())
            .catch((error) => {
            debugger;
            return rxjs_1.Observable.throw(error.json().error || 'Server error');
        });
    }
    DeleteEntry(key) {
        return this.http.post(`api/default/delete/${key}`, {})
            .map((res) => res.json())
            .catch((error) => rxjs_1.Observable.throw(error.json().error || 'Server error'));
    }
    EditEntry(key, keyword) {
        return this.http.post(`api/default/edit/${key}/${keyword}`, {})
            .map((res) => res.json())
            .catch((error) => rxjs_1.Observable.throw(error.json().error || 'Server error'));
    }
};
HomeService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HomeService);
exports.HomeService = HomeService;
//# sourceMappingURL=home.service.js.map