import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StaffType, FormAction, TableHeadlines, getStaffTypeByNum } from '../models/enums';
import { Staff } from '../models/staff';
import { StaffHttpServiceService } from '../services/staff-http-service.service';

@Component({
  selector: 'app-form-popup',
  templateUrl: './form-popup.component.html',
  styleUrls: ['./form-popup.component.css']
})
export class FormPopupComponent implements OnInit {


  @Input() isVisibile: boolean;
  @Input() type: StaffType;
  @Input() action: FormAction;
  @Input() staff: Staff;

  @Output() closeFormEvent = new EventEmitter<boolean>();

  StaffType = StaffType;
  FormAction = FormAction;
  TableHeadlines = TableHeadlines;

  isLoading: boolean = false;

  confirmDialogueData = {
    isVisible: false,
    title: "",
    message: "",
    yesFn: () => {},
    noFn: () => {}
  }

  constructor(private staffApi: StaffHttpServiceService) { }

  ngOnInit(): void {
  }

  closeForm(doReload: boolean) {
    this.closeFormEvent.emit(doReload);
  }

  confirm(title: string, message: string, yFn: () => void, nFn: () => void){
    this.confirmDialogueData = {
      isVisible: true,
      title: title,
      message: message,
      yesFn: () => {
        this.confirmDialogueData = {
          isVisible: false,
          title: "",
          message: "",
          yesFn: () => {},
          noFn: () => {}
        }
        yFn();
      },
      noFn: () => {
        this.confirmDialogueData = {
          isVisible: false,
          title: "",
          message: "",
          yesFn: () => {},
          noFn: () => {}
        }
        nFn();
      }
    }
  } 

  formFinish() {
    this.staff.staffType = getStaffTypeByNum(this.type);

    if (this.action == FormAction.create) {
      this.isLoading = true;
      this.staffApi.createStaff(this.staff).subscribe(
        res => {
          console.log("Created")
          this.isLoading = false;
          this.closeForm(true);
        }, err => {
          console.error("Not Created", err)
          this.isLoading = false;
          window.alert("Error Creating Staff");
        })
    } else if((this.action == FormAction.update)) {
      console.log(FormAction.update, this.staff)
      this.confirm("Update Staff", "Do you Want to Update Staff ?", ()=>{
        this.isLoading = true;
        this.staffApi.updateStaff(this.staff).subscribe(
          res => {
            console.log("Updated")
            this.isLoading = false;
            this.closeForm(true);
          }, err => {
            console.error("Not Updated", err)
            this.isLoading = false;
            window.alert("Error Updating Staff");
          })
      }, ()=>{})
    }
  }

}
