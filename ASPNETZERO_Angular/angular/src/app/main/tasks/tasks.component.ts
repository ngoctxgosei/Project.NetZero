import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TaskListDto, TaskServiceProxy } from '@shared/service-proxies/service-proxies';
import { TaskState } from '@shared/AppEnums';
import { AddOrEditTaskModalComponent } from '@app/main/tasks/addoredittaskmodal.component';
import { Paginator } from 'primeng/components/paginator/paginator';
@Component({
    selector: 'app-tasks',
    templateUrl: './tasks.component.html',
    styleUrls: ['./tasks.component.css']
})
export class TasksComponent extends AppComponentBase implements OnInit {
    @ViewChild('addOrEditTaskModal') addOrEditTaskModal: AddOrEditTaskModalComponent
    tasks: TaskListDto[] = [];
    selectedState: TaskState;
    stateSelectOptions = [
        { text: this.l('AllTasks'), value: undefined },
        { text: this.l('TaskState_Open'), value: TaskState.Open },
        { text: this.l('TaskState_Completed'), value: TaskState.Completed },
    ]
    @ViewChild('paginator') paginator: Paginator;
    constructor(injector: Injector, private taskService: TaskServiceProxy) {
        super(injector)

    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    ngOnInit() {
        this.getTasks();
    }
   

    getTasks() {
        this.taskService.getAll(this.selectedState as any).subscribe(result => {
            this.tasks = result.items;
        })
    }
    getTaskLabel(task: TaskListDto) {
        return task.state === TaskState.Open
            ? 'label-success'
            : 'label-default';
    }
    getTaskState(task: TaskListDto) {
        switch (task.state) {
            case TaskState.Open:
                return this.l('TaskState_Open');
            case TaskState.Completed:
                return this.l('TaskState_Completed');
            default:
                return '';
        }
    }
    showTaskModal() {
        this.addOrEditTaskModal.show();
    }

    onTaskUpdated(task: TaskListDto) {
        this.tasks.push(task);
        this.notify.success(this.l('SavedSuccessully'));
        this.getTasks();
    }

    deleteTask(task: TaskListDto): void {
        let self = this;
        self.message.confirm(
            self.l('TaskDeleteWarningMessage', task.title),
            isConfirmed => {
                if (isConfirmed) {
                    this.taskService.deleteTask(task.id).subscribe(() => {
                        this.getTasks();
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }
}
