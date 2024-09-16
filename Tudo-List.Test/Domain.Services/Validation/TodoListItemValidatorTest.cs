using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Validation.Constants;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Domain.Services.Validation
{
    public class TodoListItemValidatorTest : UnitTest
    {
        private readonly IValidator<TodoListItem> _itemValidator;
        private static User CurrentUserMock => MockData.GetCurrentUser();

        public TodoListItemValidatorTest()
        {
            _itemValidator = _serviceProvider.GetRequiredService<IValidator<TodoListItem>>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Fail_When_Registering_Item_With_Invalid_Title(string? invalidTitle)
        {
            var item = new TodoListItem()
            {
                Title = invalidTitle
            };

            Assert.Throws<ValidationException>(() => _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            }));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Fail_When_User_Tries_To_Update_His_Item_With_Invalid_Title(string invalidTitle)
        {
            var item = new TodoListItem()
            {
                Title = invalidTitle,
                UserId = CurrentUserMock.Id
            };

            Assert.Throws<ValidationException>(() => _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            }));
        }

        [Theory]
        [InlineData("Valid Title", 1)]
        [InlineData("Test", 45)]
        [InlineData("Last", 90)]
        public void Validation_Should_Fail_When_User_Tries_To_Update_An_Item_Which_Is_Not_His(string validTitle, int notCurrentUserId)
        {
            var item = new TodoListItem()
            {
                Title = validTitle,
                UserId = notCurrentUserId
            };

            Assert.Throws<ValidationException>(() => _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(45)]
        [InlineData(90)]
        public void Validation_Should_Fail_When_User_Tries_To_Delete_An_Item_Which_Is_Not_His(int notCurrentUserId)
        {
            var item = new TodoListItem()
            {
                Title = "Delete Validation Test",
                UserId = notCurrentUserId
            };

            Assert.Throws<ValidationException>(() => _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Delete);
            }));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("Valid Title")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Pass_When_Registering_Item_With_Valid_Title(string validTitle)
        {
            var item = new TodoListItem()
            {
                Title = validTitle
            };

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });
        }

        [Theory]
        [InlineData("A")]
        [InlineData("Valid Title")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Pass_When_User_Tries_To_Update_His_Item_With_Valid_Title(string validTitle)
        {
            var item = new TodoListItem()
            {
                Title = validTitle,
                UserId = CurrentUserMock.Id
            };

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            });
        }

        [Fact]
        public void Validation_Should_Pass_When_User_Tries_To_Delete_His_Item()
        {
            var item = new TodoListItem()
            {
                Title = "Delete Validation Test",
                UserId = CurrentUserMock.Id
            };

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            });
        }
    }
}
