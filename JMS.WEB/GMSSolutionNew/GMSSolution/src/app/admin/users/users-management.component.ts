import { Component, OnInit } from '@angular/core';
import { AllCommunityModules, Module } from '@ag-grid-community/all-modules';


@Component({
    selector: 'app-users-management',
    templateUrl: './users-management.component.html',
    styleUrls: ['./users-management.component.css']
})
export class UsersManagementComponent implements OnInit {


    columnDefs: { headerName: string, field: string }[];
    rowData: { make: string, model: string, price: number }[];
    modules: Module[];

    constructor() { }


    ngOnInit() {
        this.columnDefs = [
            { headerName: 'Make', field: 'make' },
            { headerName: 'Model', field: 'model' },
            { headerName: 'Price', field: 'price' }
        ];

        this.rowData = [
            { make: 'Toyota', model: 'Celica', price: 35000 },
            { make: 'Ford', model: 'Mondeo', price: 32000 },
            { make: 'Porsche', model: 'Boxter', price: 72000 }
        ];

        this.modules = AllCommunityModules;
    }

}
