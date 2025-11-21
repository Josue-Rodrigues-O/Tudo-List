import { PriorityEnum } from "../../enums/priority-enum";
import { StatusEnum } from "../../enums/status-enum";

export interface AddItem {
    title: string;
    description?: string;
    status: StatusEnum;
    priority: PriorityEnum;
}