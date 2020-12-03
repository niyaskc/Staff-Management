import { Component, Input, OnInit, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';
import { Staff, StaffWeb } from '../models/staff'
import { StaffType, TableHeadlines, SortOrder } from '../models/enums';
import { StaffHttpServiceService } from '../services/staff-http-service.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit, OnChanges {

  StaffType = StaffType;
  staffs: Staff[];
  isLoading: boolean = true;
  isSelectedAll: boolean = false;

  currentPage: number = 1;
  numPerPage: number = 2;
  currentSortItemName: TableHeadlines = TableHeadlines.Name;
  currentSortOrder: SortOrder = SortOrder.ascending;

  totalStaffs: number = 0;

  selectedStaffForDelete: number[] = [];

  TableHeadlines = TableHeadlines;
  SortOrder = SortOrder;

  Math = Math;

  @Input() selectedStaffType: StaffType;

  @Output() selectedStaffForEdit = new EventEmitter<Staff>();



  confirmDialogueData = {
    isVisible: false,
    title: "",
    message: "",
    yesFn: () => { },
    noFn: () => { }
  }

  constructor(private staffHttpServiceService: StaffHttpServiceService) {
  }

  ngOnInit(): void {

  }

  ngOnChanges(changes: SimpleChanges): void {
    for (let propName in changes) {
      if (propName === 'selectedStaffType') {
        this.reloadTable();
      }
    }
  }

  setNumPerPageFromInput(value: number){
    if (value > 0) {
      this.resetState();
      this.numPerPage = value;
      this.getStaffFromWeb();
    }
  }

  getStaffFromWeb() {
    this.isLoading = true;
    this.staffHttpServiceService.getStaffsByTypeWithPagination(this.selectedStaffType, this.currentPage,
      this.numPerPage, this.currentSortItemName, this.currentSortOrder)
      .subscribe(staffRes => {
        this.staffs = staffRes.staffs;
        this.isLoading = false;
        this.totalStaffs = staffRes.totalItems;
      }, err => {
        this.staffs = [];
        this.isLoading = false;
        this.totalStaffs = 0;
      })
  }

  editStaff(id: number) {
    console.log("Selected staff : ", this.staffs.find(s => s.id == id));
    this.selectedStaffForEdit.emit(this.staffs.find(s => s.id == id));
  }

  resetState() {
    this.isSelectedAll = false;
    this.currentPage = 1;
    this.currentSortItemName = TableHeadlines.Name;
    this.currentSortOrder = SortOrder.ascending;
    this.totalStaffs = 0;
    this.selectedStaffForDelete = [];
  }

  reloadTable() {
    this.resetState();
    this.getStaffFromWeb();
  }

  gotToPage(pageNum: number) {
    this.isSelectedAll = false;
    this.selectedStaffForDelete = [];
    if (this.currentPage !=
      pageNum && pageNum >= 1 &&
      pageNum <= Math.ceil(this.totalStaffs / this.numPerPage)) {
      this.currentPage = pageNum;
      this.getStaffFromWeb();
    }
  }

  nextPage() {
    this.gotToPage(this.currentPage + 1)
  }

  prevPage() {
    this.gotToPage(this.currentPage - 1)
  }

  changeSort(fieldName: TableHeadlines) {
    this.currentSortItemName = fieldName;
    this.currentSortOrder = this.currentSortOrder == SortOrder.ascending ? SortOrder.descending : SortOrder.ascending;
    this.getStaffFromWeb();
  }

  deleteStaff(id: number) {
    this.confirm(`Delete Staff : ${id}`, "Are your sure to delete staff ?", () => {
      this.staffHttpServiceService.deleteStaff(id).subscribe(
        res => {
          console.log("Deleted");
          this.reloadTable();
        }, err => {
          console.error("Not Deleted", err)
          window.alert(`Error Deleting Staff : ${id}`);
        })
    }, () => { })
  }

  deleteSelected() {
    if(this.selectedStaffForDelete.length < 1){
      console.log("No Staff Selected")
      return;
    }
    this.confirm(`Delete Selected Staffs`, "Are your sure to delete all the selected staffs?", () => {
      for (let i = 0; i < this.selectedStaffForDelete.length; i++) {
        this.staffHttpServiceService.deleteStaff(this.selectedStaffForDelete[i]).subscribe(
          res => {
            this.reloadTable();
          })
      }
      this.selectedStaffForDelete = [];
    }, () => { })
  }

  selectAllOrNot(isChecked: boolean) {
    if (isChecked) {
      this.staffs.forEach((staff) => {
        this.selectedStaffForDelete.push(staff.id);
      });
    } else {
      this.selectedStaffForDelete = [];
    }
  }

  selectOrNot(id: number, isChecked: boolean) {
    if (isChecked) {
      this.selectedStaffForDelete.push(id);
    } else {
      this.selectedStaffForDelete = this.selectedStaffForDelete.filter(x => x != id);
    }
  }

  confirm(title: string, message: string, yFn: () => void, nFn: () => void) {
    this.confirmDialogueData = {
      isVisible: true,
      title: title,
      message: message,
      yesFn: () => {
        this.confirmDialogueData = {
          isVisible: false,
          title: "",
          message: "",
          yesFn: () => { },
          noFn: () => { }
        }
        yFn();
      },
      noFn: () => {
        this.confirmDialogueData = {
          isVisible: false,
          title: "",
          message: "",
          yesFn: () => { },
          noFn: () => { }
        }
        nFn();
      }
    }
  }

}
