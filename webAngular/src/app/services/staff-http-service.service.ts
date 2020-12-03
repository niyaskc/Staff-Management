import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Staff, StaffWeb } from '../models/staff';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { TableHeadlines, SortOrder } from '../models/enums';

@Injectable({
  providedIn: 'root'
})
export class StaffHttpServiceService {

  private staffUrl: string = "http://localhost:56366/Staff/";
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getStaffsByTypeWithPagination(type: string, currentPage: number,
    numPerPage: number, currentSortItemName: TableHeadlines, currentSortOrder: SortOrder): Observable<StaffWeb> {
    const url = `${this.staffUrl}pages/?type=${type}&pageNo=${currentPage}&pageSize=${numPerPage}&sortByField=${currentSortItemName}&sortOrder=${currentSortOrder}`;
    return this.http.get<StaffWeb>(url);
  }

  deleteStaff(id: number): Observable<any> {
    const url = `${this.staffUrl}${id}`;
    return this.http.delete(url)
  }

  createStaff(staff: Staff): Observable<any> {
    const url = `${this.staffUrl}`;
    return this.http.post(url, staff, this.httpOptions)
  }

  updateStaff(staff: Staff): Observable<any> {
    const url = `${this.staffUrl}${staff.id}`;
    return this.http.put(url, staff, this.httpOptions)
  }


}
