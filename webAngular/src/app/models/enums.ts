export enum StaffType {
    teaching = "teaching",
    admin = "admin",
    support = "support"
}

export enum TableHeadlines {
    StaffID = "Staff ID",
    Name = "Name",
    SubjectName = "Subject Name",
    Position = "Position",
    Role = "Role"
}

export enum SortOrder {
    ascending = 'ASC',
    descending = 'DESC'
}

export enum FormAction {
    create = "Create Staff",
    update = "Update Staff"
}

export function getStaffTypeByNum(type: StaffType): number {
    switch (type) {
        case StaffType.teaching:
            return 1;
        case StaffType.admin:
            return 2;
        case StaffType.support:
            return 3;
        default:
            return -1;
    }
}