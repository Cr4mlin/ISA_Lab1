using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared.DTO
{
    /// <summary>
    /// Observable DTO для пользователя с поддержкой INotifyPropertyChanged для MVVM
    /// </summary>
    public class ObservableUserDto : INotifyPropertyChanged
    {
        private int _id;
        private string _login = string.Empty;
        private string _nickName = string.Empty;
        private int _role;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NickName
        {
            get => _nickName;
            set
            {
                if (_nickName != value)
                {
                    _nickName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Role
        {
            get => _role;
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Создает ObservableUserDto из обычного UserDto
        /// </summary>
        public static ObservableUserDto FromDto(UserDto dto)
        {
            return new ObservableUserDto
            {
                Id = dto.Id,
                Login = dto.Login,
                NickName = dto.NickName,
                Role = dto.Role
            };
        }

        /// <summary>
        /// Конвертирует обратно в обычный UserDto
        /// </summary>
        public UserDto ToDto()
        {
            return new UserDto
            {
                Id = Id,
                Login = Login,
                NickName = NickName,
                Role = Role
            };
        }
    }
}
