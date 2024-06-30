﻿using Service.DTOs.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Teachers
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public List<GroupDto> Groups { get; set; }
    }
}
