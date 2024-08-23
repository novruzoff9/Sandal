#!/bin/bash

# Renk tanımları
COLOR_RESET="\033[0m"
COLOR_BLUE="\033[34m"
COLOR_GREEN="\033[32m"

# Klasör yolları
Models_folder_path="C:/Users/Novruzoff/source/repos/Sandal/EntityLayer/Concrete"
DAL_Interface_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/Abstract"
DAL_concrete_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/Concrete"
BL_Interface_folder_path="C:/Users/Novruzoff/source/repos/Sandal/BusinessLayer/Abstract"
BL_concrete_folder_path="C:/Users/Novruzoff/source/repos/Sandal/BusinessLayer/Concrete"

# Klasörleri oluştur
create_folder() {
    local path=$1
    if [ ! -d "$path" ]; then
        mkdir -p "$path"
        echo -e "${COLOR_GREEN}Klasör oluşturuldu: $path${COLOR_RESET}"
    else
        echo -e "${COLOR_BLUE}Klasör zaten mevcut: $path${COLOR_RESET}"
    fi
}

create_folder "$DAL_Interface_folder_path" &
create_folder "$DAL_concrete_folder_path" &
create_folder "$BL_Interface_folder_path" &
create_folder "$BL_concrete_folder_path" &
wait

# Dosya oluşturma işlemini tanımla
create_files() {
    local file=$1
    local class_name=$(basename "$file" .cs)
    
    # Data Access Layer dosyalarını oluştur
    local dal_interface="$DAL_Interface_folder_path/I${class_name}Repository.cs"
    local dal_concrete="$DAL_concrete_folder_path/${class_name}Repository.cs"
    
    if [ ! -f "$dal_interface" ]; then
        cat <<EOL >"$dal_interface"
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface I${class_name}Repository : IGenericRepository<${class_name}>
    {
    }
}
EOL
        echo -e "${COLOR_GREEN}Dosya oluşturuldu: $dal_interface${COLOR_RESET}"
    else
        echo -e "${COLOR_BLUE}Dosya zaten mevcut: $dal_interface${COLOR_RESET}"
    fi

    if [ ! -f "$dal_concrete" ]; then
        cat <<EOL >"$dal_concrete"
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete
{
    public class ${class_name}Repository : GenericRepository<${class_name}>, I${class_name}Repository
    {
        public ${class_name}Repository(SandalContext context) : base(context)
        {
        }
    }
}
EOL
        echo -e "${COLOR_GREEN}Dosya oluşturuldu: $dal_concrete${COLOR_RESET}"
    else
        echo -e "${COLOR_BLUE}Dosya zaten mevcut: $dal_concrete${COLOR_RESET}"
    fi

    # Business Layer dosyalarını oluştur
    local bl_interface="$BL_Interface_folder_path/I${class_name}Service.cs"
    local bl_concrete="$BL_concrete_folder_path/${class_name}Service.cs"
    
    if [ ! -f "$bl_interface" ]; then
        cat <<EOL >"$bl_interface"
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface I${class_name}Service
    {
        void Create${class_name}(${class_name} entity);
        void Update${class_name}(${class_name} entity);
        void Delete${class_name}(${class_name} entity);
        ${class_name} Get${class_name}ById(int id);
        List<${class_name}> GetAll${class_name}();
        Task<List<${class_name}>> GetAll${class_name}Async();
    }
}
EOL
        echo -e "${COLOR_GREEN}Dosya oluşturuldu: $bl_interface${COLOR_RESET}"
    else
        echo -e "${COLOR_BLUE}Dosya zaten mevcut: $bl_interface${COLOR_RESET}"
    fi

    if [ ! -f "$bl_concrete" ]; then
        cat <<EOL >"$bl_concrete"
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ${class_name}Service : I${class_name}Service
    {
        private readonly I${class_name}Repository _${class_name}Repository;

        public ${class_name}Service(I${class_name}Repository ${class_name}Repository)
        {
            _${class_name}Repository = ${class_name}Repository;
        }

        public void Create${class_name}(${class_name} entity)
        {
            _${class_name}Repository.Insert(entity);
        }

        public void Delete${class_name}(${class_name} entity)
        {
            _${class_name}Repository.Delete(entity);
        }

        public List<${class_name}> GetAll${class_name}()
        {
            return _${class_name}Repository.GetAll().ToList();
        }

        public async Task<List<${class_name}>> GetAll${class_name}Async()
        {
            return await _${class_name}Repository.GetAllAsync();
        }

        public ${class_name} Get${class_name}ById(int id)
        {
            return _${class_name}Repository.GetById(id);
        }

        public void Update${class_name}(${class_name} entity)
        {
            _${class_name}Repository.Update(entity);
        }
    }
}
EOL
        echo -e "${COLOR_GREEN}Dosya oluşturuldu: $bl_concrete${COLOR_RESET}"
    else
        echo -e "${COLOR_BLUE}Dosya zaten mevcut: $bl_concrete${COLOR_RESET}"
    fi
}

# Dosyaları işle
for file in "$Models_folder_path"/*.cs; do
    create_files "$file" &
done
wait

echo -e "${COLOR_GREEN}Tüm dosyalar oluşturuldu.${COLOR_RESET}"

# Kullanıcıdan bir tuşa basmasını bekle
read -p "Devam etmek için herhangi bir tuşa basın..." -n1 -s
