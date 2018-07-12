import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TasksComponent } from '@app/main/tasks/tasks.component';
import { EmployeesComponent } from '@app/main/employees/employees.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'tasks', component: TasksComponent },
                    { path: 'employees', component: EmployeesComponent }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
