import { Component, OnInit } from '@angular/core';

import { HomeService } from "../../core/services/home.service";

@Component({
    selector: 'home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

    result: any[] = [];        
	keyword: string = '';

    constructor(private hserv: HomeService) 
	{
        this.hserv.GetEntries(this.keyword).subscribe(response => this.result = response);
    }

    ngOnInit(): void {

    }

	add(){

		if ( !this.check() )
		{
			alert('The word is not valid. ');
			return;
		}
		else
		{
			this.hserv.AddEntry(this.keyword).subscribe(response => this.result = response);
		}
	}

	edit(value: string)
	{
		
		console.log(value)
		this.hserv.EditEntry(value).subscribe(response => this.result = response);



	}

	delete(key: string)
	{
		console.log('deleting for: ' + key);

		this.hserv.DeleteEntry(key).subscribe(response => this.result = response);

	}

	handleKeyUp(e: any){
		
		this.keyword = this.keyword || '';

		// if(e.keyCode === 13)
		if(this.keyword.length > 2)
		{
			this.hserv.GetEntries(this.keyword).subscribe(response => this.result = response);
		}

		console.log("typed with " + this.keyword)
	 }

	 check(): boolean
	 {
		 debugger
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
		let formatAddress : RegExp = /^[^@\s]+@[^@\s]+\.[^@\s]+$/
		let formatdomain: RegExp = /^((?!-)[A-Za-z0-9-]{1, 63}(?<!-)\\.)+[A-Za-z]{2, 6}$/
		
		if (
			formatAddress.test(this.keyword)
			||
			formatdomain.test(this.keyword)
			)
		{
			return true;
		}
		else
		{
			return false;
		}



	 }
}