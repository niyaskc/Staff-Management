import { Component, ViewChild } from '@angular/core';
import { StaffHttpServiceService } from './services/staff-http-service.service';
import { StaffType, FormAction, getStaffTypeByNum } from './models/enums';
import { Staff } from './models/staff';
import { TableComponent } from './table/table.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  @ViewChild(TableComponent) tableComp: TableComponent; 

  title = 'Staff Management';
  selectedStaffType: StaffType = StaffType.teaching;
  selectedStaff: Staff = {
    id: 0,
    name: "",
    staffType: getStaffTypeByNum(this.selectedStaffType),
    subjectName: "",
    position: "",
    role: ""
  }

  FormAction = FormAction;
  StaffType = StaffType;

  formAction: FormAction = FormAction.create;
  formIsVisible: Boolean = false;


  constructor(private staffHttpServiceService: StaffHttpServiceService) { }

  showForm(action: FormAction) {
    this.formAction = action;
    if (action == FormAction.create) {
      this.selectedStaff = {
        id: 0,
        name: "",
        staffType: getStaffTypeByNum(this.selectedStaffType),
        subjectName: "",
        position: "",
        role: ""
      }
    }
    this.formIsVisible = true;
  }

  closeForm = (isReload) => {
    if(isReload){
        this.tableComp.reloadTable();
    }
    this.formIsVisible = false;
  }

  editStaff(staff: Staff) {
    this.selectedStaff = JSON.parse(JSON.stringify(staff));
    this.showForm(FormAction.update);
  }

  deleteSelectedFromTable = ()=>{
    this.tableComp.deleteSelected();
  }
}
