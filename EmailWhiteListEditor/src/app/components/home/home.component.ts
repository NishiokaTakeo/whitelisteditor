import { Component, OnInit } from '@angular/core';

import { HomeService } from "../../core/services/home.service";

@Component({
    selector: 'home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

    result: any[] = [];        
    backup: any[] = [];        
	keyword: string = '';

    constructor(private hserv: HomeService) 
	{
        this.init();
    }

    ngOnInit(): void {

    }
	
	init()
	{
		this.hserv.GetAllEntries().subscribe(response => this.copyToBackup(this.result = response) );
	}

	copyToBackup(arr:any[])
	{
		this.backup = JSON.parse(JSON.stringify(arr));		
	}

	add(){

		if ( !this.check() )
		{
			alert('The word is not valid. ');
			return;
		}
		else
		{
			this.hserv.AddEntry(this.keyword).subscribe(response => this.copyToBackup(this.result = response));
		}
	}

	edit(index: number)
	{
		let key = this.backup[index]
		let value = this.result[index]
		console.log(value)
		
		this.hserv.EditEntry(key,value)
		
		.subscribe(response => {

			this.copyToBackup(this.result = response);

			this.init();

		});

	}

	delete(key: string, index: number)
	{
		let originalValue = this.backup[index];
		console.log('deleting for: ' + originalValue);
		if ( confirm(`Would you like to delete '${originalValue}' ?`))
		{
			this.hserv.DeleteEntry(originalValue).subscribe(response => this.copyToBackup(this.result = response));

			// TODO: notify success full message
		}
		else
		{
			this.result[index] = this.backup[index];
		}
	}

	handleKeyUp(e: any){
		
		this.keyword = this.keyword || '';

		// if(e.keyCode === 13)
		if(this.keyword.length > 2)
		{
			this.hserv.GetEntries(this.keyword).subscribe(response => this.copyToBackup(this.result = response));
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
		let formatdomain: RegExp = /^[^@\s]+\.[^@\s]+$/
		
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

	 trackByFn(index: any, item: any) {
		return index;
	 }
}