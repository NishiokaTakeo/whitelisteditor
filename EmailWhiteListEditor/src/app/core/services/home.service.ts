import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs';

@Injectable()
export class HomeService {
    
    constructor(private http: Http) {
    
    }


    GetHomeMessage(): Observable<any[]> {
        return this.http.get(`api/default`)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
    }

    GetEntries(keyword: string): Observable<any[]> {
        
		return this.http.post(`api/default/entries/`+keyword,"")
            .map((res: Response) => {
				return res.json();
			})
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
    }
    
	GetAllEntries(): Observable<any[]> {
        
		return this.http.post(`api/default/entries/all`,"")
            .map((res: Response) => {
				return res.json();
			})
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
    }

    AddEntry(keyword: string): Observable<any[]> {
        
		return this.http.post(`api/default/add/`+ keyword,{})
            .map((res: Response) => res.json())
            .catch((error: any) => 
			
			{
				debugger
				return Observable.throw(error.json().error || 'Server error'); 
			})
    }


    DeleteEntry(key:string): Observable<any[]> {
        
		return this.http.post(`api/default/delete/${key}`,{})
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
    }

	EditEntry(key: string,keyword: string): Observable<any[]> {
        
		return this.http.post(`api/default/edit/${key}/${keyword}`,{})
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error.json().error || 'Server error'))
    }
    
}