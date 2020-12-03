export interface Staff {
  id: number;
  name: string;
  staffType: number;
  subjectName?: string;
  position?: string;
  role?: string;
}

export interface StaffWeb {

  staffs: Staff[];
  totalItems: number;
}