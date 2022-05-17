import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Occupation } from '../_dto/occupation';
import { OccupationResult } from '../_dto/occupation-result';

@Injectable({
  providedIn: 'root'
})
export class OccupationService {

  apiUrl: string = '/occupation/api/';

  constructor(private http: HttpClient) { }

  getOccupations(cached: boolean): Observable<OccupationResult> {
    return this.http.get<OccupationResult>(`${this.apiUrl}`+cached);
  }
}
